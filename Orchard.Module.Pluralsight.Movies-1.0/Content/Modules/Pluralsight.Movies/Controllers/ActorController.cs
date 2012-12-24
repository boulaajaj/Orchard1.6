using System;
using System.Linq;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Themes;
using Pluralsight.Movies.Models;
using Pluralsight.Movies.ViewModels;

namespace Pluralsight.Movies.Controllers {
    [Themed]
    public class ActorController : Controller {
        private readonly IRepository<ActorRecord> _actorRepository;
        private readonly IContentManager _contentManager;

        public ActorController(IRepository<ActorRecord> actorRepository, IContentManager contentManager) {
            _actorRepository = actorRepository;
            _contentManager = contentManager;
        }

        [HttpGet]
        public ActionResult Details(int actorId) {
            return ActorDetails(() => _actorRepository.Get(actorId));
        }

        [HttpGet]
        public ActionResult DetailsByName(string actorName) {
            return ActorDetails(() => _actorRepository.Fetch(a => a.Name == actorName).SingleOrDefault());
        }

        private ActionResult ActorDetails(Func<ActorRecord> getActor) {
            var actor = getActor();
            if (actor == null) {
                return HttpNotFound();
            }

            var movieIds = actor.ActorMovies.Select(m => m.MoviePartRecord.Id);
            var movies = _contentManager.GetMany<MoviePart>(movieIds, VersionOptions.Published, QueryHints.Empty)
                .OrderByDescending(m => m.YearReleased).ToList();

            var viewModel = new ActorViewModel {Name = actor.Name, Movies = movies};
            return View("Details", viewModel);
        }
    }
}