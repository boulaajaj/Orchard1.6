using Orchard.Localization;
using Orchard.UI.Navigation;

namespace Richinoz.Paypal
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }

        public string MenuName
        {
            get { return "admin"; }
        }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.Add(T("PaypalAdmin"), "5", BuildMenu);
        }

        private void BuildMenu(NavigationItemBuilder menu)
        {
           // Admin/Contents/List/Movie
          
            menu.Add(T("Paypal Lookup"), "1.2", item =>
                item.Action("PostToPaypal", "Paypal", new { area = "Richinoz.Paypal" }));

        }
    }
}