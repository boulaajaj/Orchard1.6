using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.Data;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Shapes;
using Orchard.Localization;
using Orchard.Settings;
using Orchard.UI.Admin;
using Orchard.UI.Navigation;
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
        }

        public Localizer T { get; set; }
        public dynamic Shape { get; set; }

        [Admin]
        public ActionResult Index(PagerParameters pagerParameters) {
             var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);
             var count = _actorRepository.Table.Count();
             var actors = _actorRepository.Table.Skip((pager.Page - 1)*pager.PageSize);
             var pagerShape= Shape.Pager(pager).TotalItemCount(count);

             var viewModel = new ActorsIndexViewModel { Actors = actors, Pager = pagerShape };

             return View(viewModel);
         }
    }
}