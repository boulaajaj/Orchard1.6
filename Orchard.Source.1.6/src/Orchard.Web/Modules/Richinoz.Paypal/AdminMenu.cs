using Orchard.Core.Contents;
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
            builder.Add(T("Paypal"), "5", BuildMenu);
        }

        private void BuildMenu(NavigationItemBuilder menu)
        {
           // Admin/Contents/List/Movie
            menu.Add(T("List"), "1.0", item =>
                item.Action("List", "Admin", new { area = "Contents", id = "Product" }));

            //menu.Add(T("New Movie"), "1.1", item =>
            //    item.Action("Create", "Admin", new { area = "Contents", id = "Movie" }));

            //menu.Add(T("Movie Lookup"), "1.2", item =>
            //    item.Action("Index", "MovieLookup", new { area = "Pluralsight.Movies" }).Permission(Permissions.LookupMovie));

            //menu.Add(T("Actors"), "2.0", item =>
            //    item.Action("Index", "ActorsAdmin", new { area = "Pluralsight.Movies" }));

            ////Admin/Contents/List/PrettyGallery
            // menu.Add(T("Image Gallery"), "3.0", item =>
            //    item.Action("List", "Admin", new { area = "Contents", id = "PrettyGallery" }));
        }
    }
}