using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Shapes;
using Orchard.Localization;
using Orchard.Settings;
using Orchard.UI.Admin;
using Orchard.UI.Navigation;
using Orchard.UI.Notify;
using Pluralsight.Movies.Models;
using Pluralsight.Movies.ViewModels;

namespace Pluralsight.Movies.Controllers {
    public class ActorsAdminController:Controller {
        private readonly IOrchardServices _orchardServices;
        private readonly ISiteService _siteService;
        private readonly IRepository<ActorRecord> _actorRepository;

        public ActorsAdminController(
            IOrchardServices orchardServices,
            ISiteService siteService,
            IRepository<ActorRecord> actorRepository,
            IShapeFactory shapeFactory)
        {
            _orchardServices = orchardServices;
            _siteService = siteService;
            _actorRepository = actorRepository;
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }
        public dynamic Shape { get; set; }

        [Admin]
        public ActionResult Index(PagerParameters pagerParameters) {
             var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);
             var count = _actorRepository.Table.Count();
             var actors = _actorRepository.Table
                 .OrderBy(a=>a.Name)
                 .Skip((pager.Page - 1)*pager.PageSize)
                 .Take(pager.PageSize)
                 .ToList();
             var pagerShape= Shape.Pager(pager).TotalItemCount(count);

             var viewModel = new ActorsIndexViewModel { Actors = actors, Pager = pagerShape };

             return View(viewModel);
         }

        [HttpGet, Admin]
        public ActionResult Create() {
            return View(new CreateActorViewModel());
        }

        [HttpPost, ActionName("Create"), Admin]
        public ActionResult CreatePOST(CreateActorViewModel viewModel) {
            if(!ModelState.IsValid) {
                return View(viewModel);
            }
            _actorRepository.Create(new ActorRecord(){Name = viewModel.Name});
            _orchardServices.Notifier.Add(NotifyType.Information, T("Created the actor {0}", viewModel.Name));
            return RedirectToAction("Index");
        }

        [HttpGet, Admin]
        public ActionResult Edit(int actorId)
        {
            var actor = _actorRepository.Get(actorId);
            if (actor == null)
            {
                return new HttpNotFoundResult("Could not find the actor with id " + actorId);
            }
            var actorMovies = _orchardServices.ContentManager.GetMany<MoviePart>(
                actor.ActorMovies.Select(m => m.MoviePartRecord.Id), VersionOptions.Published, QueryHints.Empty);
            return View(new EditActorViewModel { Id = actorId, Name = actor.Name, Movies = actorMovies });
        }

        [HttpPost, ActionName("Edit"), Admin]
        public ActionResult EditPOST(EditActorViewModel viewModel)
        {
            var actor = _actorRepository.Get(viewModel.Id);

            if (!ModelState.IsValid)
            {
                viewModel.Movies = _orchardServices.ContentManager.GetMany<MoviePart>(actor.ActorMovies.Select(m => m.MoviePartRecord.Id), VersionOptions.Published, QueryHints.Empty);
                return View("Edit", viewModel);
            }

            actor.Name = viewModel.Name;
            _actorRepository.Update(actor);
            _orchardServices.Notifier.Add(NotifyType.Information, T("Saved {0}", viewModel.Name));
            return RedirectToAction("Index");
        }

        [Admin]
        public ActionResult Delete(int actorId, string name) {
            return View(new EditActorViewModel { Id = actorId, Name = name });
       
        }

        [HttpPost,ActionName("Delete"), Admin]
        public ActionResult DeletePOST(int actorId)
        {
            var actor = _actorRepository.Get(actorId);
            if (actor == null)
            {
                return new HttpNotFoundResult("Could not find the actor with id " + actorId);
            }
            _actorRepository.Delete(actor);
            _orchardServices.Notifier.Add(NotifyType.Information, T("The actor {0} has been deleted", actor.Name));
            return RedirectToAction("Index");
        }
    }
}