using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Data;
using Pluralsight.Movies.Models;
using Pluralsight.Movies.Services;
using Pluralsight.Movies.ViewModels;

namespace Pluralsight.Movies.Drivers {
    public class MoviePartDriver : ContentPartDriver<MoviePart> {
        private readonly IRepository<ActorRecord> _actorRepository;
        private readonly IMovieService _movieService;

        public MoviePartDriver(IRepository<ActorRecord> actorRepository,
            IMovieService movieService) {
            _actorRepository = actorRepository;
            _movieService = movieService;
        }

        protected override string Prefix {
            get { return "Movie"; }
        }

        protected override DriverResult Display(MoviePart part, string displayType, dynamic shapeHelper) {
            return Combined(
                ContentShape("Parts_Movie",() => shapeHelper.Parts_Movie(MoviePart: part)),
                ContentShape("Parts_Movie_Tagline", () => shapeHelper.Parts_Movie_Tagline(MoviePart: part, Tagline: part.Tagline))
                );
        }

        // GET
        protected override DriverResult Editor(MoviePart part, dynamic shapeHelper) {
            return ContentShape("Parts_Movie_Edit", () =>
                shapeHelper.EditorTemplate(TemplateName: "Parts/Movie", Model: BuildEditorViewModel(part), Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(MoviePart part, IUpdateModel updater, dynamic shapeHelper) {
            var viewModel = new MovieEditViewModel();
            updater.TryUpdateModel(viewModel, Prefix, null, new [] { "AllActors" });
            _movieService.UpdateMovie(viewModel, part);
            return Editor(part, shapeHelper);
        }

        private MovieEditViewModel BuildEditorViewModel(MoviePart part) {
            return new MovieEditViewModel {
                IMDB_Id = part.IMDB_Id,
                YearReleased = part.YearReleased,
                Rating = part.Rating,
                Tagline = part.Tagline,
                Keywords = part.Keywords,
                Actors = part.Cast.Select(c => c.Id).ToList(),
                AllActors = _actorRepository.Table.OrderBy(a => a.Name).ToList()
            };
        }
    }
}