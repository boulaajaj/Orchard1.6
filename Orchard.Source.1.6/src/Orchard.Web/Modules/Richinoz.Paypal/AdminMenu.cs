using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Navigation;

namespace Richinoz.Paypal
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("modules")
              .Add(T("Orders"), "11",
                          menu => menu.Add(T("Orders"), "11",
                              item => item.Action("List", "Admin", new { area = "Contents", id = "Order" })));

        }
    }
}