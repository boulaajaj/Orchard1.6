using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.UI.Navigation;

namespace Richinoz.Paypal {
   // [OrchardFeature("Nwazet.Commerce")]
    public class OrderAdminMenu : INavigationProvider {
        public string MenuName {
            get { return "admin"; }
        }

        public OrderAdminMenu()
        {
            T = NullLocalizer.Instance;
        }

        private Localizer T { get; set; }

        public void GetNavigation(NavigationBuilder builder) {
            builder
                .AddImageSet("modules")
                .Add(item => item
                    .Caption(T("Orders"))
                    .Position("2")
                    .LinkToFirstChild(true)

                    .Add(subItem => subItem
                        .Caption(T("Orders"))
                        .Position("2.0")
                        .Action("List", "OrderAdmin", new { area = "Richinoz.Paypal" })
                    )
                );
        }
    }
}
