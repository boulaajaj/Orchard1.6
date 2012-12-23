using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Pluralsight.Movies.Models;

namespace Pluralsight.Movies.Drivers {
    public class MoviePartDriver:ContentPartDriver<MoviePart> {


        protected override string Prefix
        {
            get { return "Movie"; }
        }

        protected override DriverResult Display(MoviePart part, string displayType, dynamic shapeHelper)
        {
            //displayType could be Detail, Summary, Summary_Admin
            return ContentShape("Parts_Movie", () => shapeHelper.Parts_Movie(MoviePart: part));
        }
       //get
        protected override DriverResult Editor(MoviePart part, dynamic shapeHelper) {
            return ContentShape("Parts_Movie_Edit", () =>
                                                    shapeHelper.EditorTemplate(TemplateName: "Parts/Movie", Model: part, Prefix: Prefix));
        }

        //post
        protected override DriverResult Editor(MoviePart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}