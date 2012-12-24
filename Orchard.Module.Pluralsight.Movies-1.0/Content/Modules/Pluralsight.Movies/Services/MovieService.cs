using System;
using System.Collections.Generic;
using System.Linq;
using Contrib.Taxonomies.Models;
using Contrib.Taxonomies.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Core.Title.Models;
using Orchard.Data;
using Pluralsight.Movies.Models;
using Pluralsight.Movies.ViewModels;
using TheMovieDb;

namespace Pluralsight.Movies.Services {
    public class MovieService : IMovieService {
	    private readonly IRepository<ActorRecord> _actorRepository;
	    private readonly IRepository<MovieActorRecord> _movieActorRepository;
        private readonly IOrchardServices _orchardServices;
        private readonly ITaxonomyService _taxonomyService;

        private Lazy<TmdbApi> _tmdbAPI;

        public MovieService(IRepository<ActorRecord> actorRepository,
		    IRepository<MovieActorRecord> movieActorRepository,
            IOrchardServices orchardServices,
            ITaxonomyService taxonomyService) {
		    _actorRepository = actorRepository;
		    _movieActorRepository = movieActorRepository;
	        _orchardServices = orchardServices;
            _taxonomyService = taxonomyService;

            _tmdbAPI = new Lazy<TmdbApi>(() =>
                new TmdbApi(_orchardServices.WorkContext.CurrentSite.As<MovieSettingsPart>().TMDB_APIKey));
        }

	    public void UpdateMovie(MovieEditViewModel model, MoviePart part) {
		    part.IMDB_Id = model.IMDB_Id;
		    part.YearReleased = model.YearReleased;
		    part.Rating = model.Rating;
	        part.Tagline = model.Tagline;
	        part.Keywords = model.Keywords;

		    var oldCast = _movieActorRepository.Fetch(ma => ma.MoviePartRecord.Id == part.Id).Select(r => r.ActorRecord.Id).ToList();
		    foreach (var oldActorId in oldCast.Except(model.Actors)) {
			    _movieActorRepository.Delete(_movieActorRepository.Get(r => r.ActorRecord.Id == oldActorId));
		    }
		    foreach (var newActorId in model.Actors.Except(oldCast)) {
			    var actor = _actorRepository.Get(newActorId);
			    _movieActorRepository.Create(new MovieActorRecord {ActorRecord = actor, MoviePartRecord = part.Record});
		    }
	    }

        public void ImportMovie(int tmdbMovieId) {
            try {
                var movieInfo = _tmdbAPI.Value.GetMovieInfo(tmdbMovieId);
                var movie = _orchardServices.ContentManager.New("Movie");

                movie.As<TitlePart>().Title = movieInfo.Name;
                movie.As<BodyPart>().Text = movieInfo.Overview;
                if (movieInfo.Released.Contains("-")) {
                    //TMDB Released format is YYYY-MM-DD
                    movie.As<MoviePart>().YearReleased = int.Parse(movieInfo.Released.Split('-')[0]);
                }
                movie.As<MoviePart>().Rating = (MPAARating)Enum.Parse(typeof (MPAARating), movieInfo.Certification.Replace("-", ""));
                movie.As<MoviePart>().IMDB_Id = movieInfo.ImdbId;
                movie.As<MoviePart>().Tagline = movieInfo.Tagline;
                movie.As<MoviePart>().Keywords = String.Join(",", movieInfo.Keywords.Select(k => k.Trim()));

                AssignGenres(movie, movieInfo);
                AssignActors(movie.As<MoviePart>(), movieInfo);
                _orchardServices.ContentManager.Create(movie, VersionOptions.Published);
            }
            catch (Exception) {
                _orchardServices.TransactionManager.Cancel();
                throw;
            }
        }

        public void ImportMovies(IEnumerable<int> tmdbMovieIds) {
            foreach (var movieId in tmdbMovieIds) {
                ImportMovie(movieId);
            }
        }

        private void AssignGenres(ContentItem movie, TmdbMovie tmdbMovie) {
            var genreTaxonomy = _taxonomyService.GetTaxonomyByName("Genre");
            var allGenres = _taxonomyService.GetTerms(genreTaxonomy.Id);
            var movieGenres = new List<TermPart>();

            foreach (var tmdbGenre in tmdbMovie.Genres) {
                var genre = allGenres.SingleOrDefault(g => g.Name == tmdbGenre.Name);
                if (genre == null) {
                    genre = _taxonomyService.NewTerm(genreTaxonomy);
                    genre.Name = tmdbGenre.Name;
                    _orchardServices.ContentManager.Create(genre, VersionOptions.Published);
                }
                movieGenres.Add(genre);
            }
            _taxonomyService.UpdateTerms(movie, movieGenres, "Genre");
        }

        private void AssignActors(MoviePart movie, TmdbMovie movieInfo) {
            var movieActors = new List<MovieActorRecord>();

            foreach (var tmdbCastMember in movieInfo.Cast.Where(c => c.Job == "Actor").OrderBy(c => c.Order).Take(5)) {
                var actor = _actorRepository.Fetch(a => a.Name == tmdbCastMember.Name).SingleOrDefault();
                if (actor == null) {
                    actor = new ActorRecord {Name = tmdbCastMember.Name};
                    _actorRepository.Create(actor);
                }
                movieActors.Add(new MovieActorRecord {ActorRecord = actor, MoviePartRecord = movie.Record});
            }
            movieActors.ForEach(ma => _movieActorRepository.Create(ma));
        }
    }
}