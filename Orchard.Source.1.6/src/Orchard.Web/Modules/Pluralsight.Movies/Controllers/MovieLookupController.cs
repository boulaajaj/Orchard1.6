using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Admin;
using Orchard.UI.Notify;
using Pluralsight.Movies.Models;
using Pluralsight.Movies.Services;
using Pluralsight.Movies.ViewModels;
using TheMovieDb;

namespace Pluralsight.Movies.Controllers
{
    [Admin]
    public class MovieLookupController : Controller
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IMovieService _movieService;
        private readonly IAuthorizer _authorizer;

        public MovieLookupController(
            IOrchardServices orchardServices,
            IMovieService movieService,
            IAuthorizer authorizer)
        {
            _orchardServices = orchardServices;
            _movieService = movieService;
            _authorizer = authorizer;
        }

        public Localizer T { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            if (!_authorizer.Authorize(Permissions.LookupMovie))
            {
                return new HttpUnauthorizedResult();
            }
            return View(new MovieLookupViewModel());
        }

        [HttpPost, ActionName("Index")]
        public ActionResult IndexPost(MovieLookupViewModel viewModel)
        {
            if (!_authorizer.Authorize(Permissions.LookupMovie))
            {
                return new HttpUnauthorizedResult();
            }

            if ((string.IsNullOrWhiteSpace(viewModel.MovieTitle) && string.IsNullOrWhiteSpace(viewModel.IMDB_Id)) ||
                (!string.IsNullOrWhiteSpace(viewModel.MovieTitle) && !string.IsNullOrWhiteSpace(viewModel.IMDB_Id)))
            {
                ModelState.AddModelError("MovieTitle", T("You must enter either a Title or an IMDB ID to lookup, but not both.").Text);
                return View("Index", viewModel);
            }

            IEnumerable<TmdbMovie> movies;
            var tmdbApi = new TmdbApi(_orchardServices.WorkContext.CurrentSite.As<MovieSettingsPart>().TMDB_APIKey);

            try
            {
                if (!string.IsNullOrWhiteSpace(viewModel.MovieTitle))
                {
                    movies = tmdbApi.MovieSearch(viewModel.MovieTitle);
                }
                else
                {
                    movies = tmdbApi.MovieSearchByImdb(viewModel.IMDB_Id);
                }
                if (movies.Any())
                {
                    viewModel.MovieResults = movies.Select(r =>
                        new MovieResult
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Released = r.Released
                        });
                }
                else
                {
                    viewModel.NoMatch = true;
                }
            }
            catch (Exception ex)
            {
                viewModel.NoMatch = true;
            }

            return View("Index", viewModel);
        }

        [HttpPost]
        public ActionResult Import(IEnumerable<int> selectedMovieIDs)
        {
            if (selectedMovieIDs.Any())
            {
                _movieService.ImportMovies(selectedMovieIDs);
                _orchardServices.Notifier.Add(NotifyType.Information, T("Imported the selected movies"));
            }
            return RedirectToAction("Index");
        }
    }
}