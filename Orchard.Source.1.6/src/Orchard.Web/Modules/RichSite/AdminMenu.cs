using Orchard.Localization;
using Orchard.UI.Navigation;

namespace RichSite
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
            builder.Add(T("Commerce"), "5", BuildMenu);
        }

        private void BuildMenu(NavigationItemBuilder menu)
        {
           // Admin/Contents/List/Product
            menu.Add(T("Product List"), "1.0", item =>
                item.Action("List", "Admin", new { area = "Contents", id = "Product" }));

        }
    }
}