using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nwazet.Commerce.Models;
using Nwazet.Commerce.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Core.Contents.ViewModels;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.UI.Admin;
using Orchard.Settings;
using Orchard.UI.Navigation;
using Richinoz.Paypal.Enums;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Controllers {
    //[OrchardFeature("Nwazet.Commerce")]
    [ValidateInput(false), Admin]
    public class OrderAdminController : Controller {
        private readonly IContentManager _contentManager;
        private readonly ISiteService _siteService;
        private readonly IWorkContextAccessor _wca;

        public OrderAdminController(
            IOrchardServices services,
            IContentManager contentManager,
            ISiteService siteService,
            IWorkContextAccessor wca,
            IShapeFactory shapeFactory) {
            Services = services;
            _contentManager = contentManager;
            _siteService = siteService;
            _wca = wca;
            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }

        dynamic Shape { get; set; }
        public Localizer T { get; set; }
        public IOrchardServices Services { get; set; }

        public ActionResult List(ListContentsViewModel model, PagerParameters pagerParameters) {
            var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);
            var query = _contentManager.Query<OrderPart, OrderPartRecord>(VersionOptions.Latest);

            switch (model.Options.OrderBy) {
                case ContentsOrder.Modified:
                    query.OrderByDescending<CommonPartRecord>(cr => cr.ModifiedUtc);
                    break;
                case ContentsOrder.Published:
                    query.OrderByDescending<CommonPartRecord>(cr => cr.PublishedUtc);
                    break;
                case ContentsOrder.Created:
                    query.OrderByDescending<CommonPartRecord>(cr => cr.CreatedUtc);
                    break;
            }

            var pagerShape = Shape.Pager(pager).TotalItemCount(query.Count());
            var pageOfContentItems = query.Slice(pager.GetStartIndex(), pager.PageSize).ToList();

            var list = Shape.List();
            list.AddRange(pageOfContentItems.Select(ci => _contentManager.BuildDisplay(ci, "SummaryAdmin")));

            dynamic viewModel = Shape.ViewModel()
                .ContentItems(list)
                .Pager(pagerShape)
                .Options(model.Options);

            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)viewModel);
        }

        public ActionResult ListOrder(ListContentsViewModel model, PagerParameters pagerParameters)
        {
            var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);
            var query = _contentManager.Query<OrderPart, OrderPartRecord>(VersionOptions.Latest);

            switch (model.Options.OrderBy)
            {
                case ContentsOrder.Modified:
                    query.OrderByDescending<CommonPartRecord>(cr => cr.ModifiedUtc);
                    break;
                case ContentsOrder.Published:
                    query.OrderByDescending<CommonPartRecord>(cr => cr.PublishedUtc);
                    break;
                case ContentsOrder.Created:
                    query.OrderByDescending<CommonPartRecord>(cr => cr.CreatedUtc);
                    break;
            }

            //query.Where<OrderPartRecord>(cr => cr.Status == OrderStatus.Paid.ToString());
            query.Where<OrderPartRecord>(cr => cr.TransactionId != null);

            var pagerShape = Shape.Pager(pager).TotalItemCount(query.Count());
            var pageOfContentItems = query.Slice(pager.GetStartIndex(), pager.PageSize).ToList();

            var list = Shape.List();
            list.AddRange(pageOfContentItems.Select(ci => _contentManager.BuildDisplay(ci, "SummaryAdmin")));

            dynamic viewModel = Shape.ViewModel()
                .ContentItems(list)
                .Pager(pagerShape)
                .Options(model.Options);

            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View("List",(object)viewModel);
        }

    }
}