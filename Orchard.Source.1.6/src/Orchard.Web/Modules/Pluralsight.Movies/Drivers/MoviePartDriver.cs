using Orchard.ContentManagement.Drivers;
using Pluralsight.Movies.Models;

namespace Pluralsight.Movies.Drivers {
    public class MoviePartDriver:ContentPartDriver<MoviePart> {


        protected override string Prefix
        {
            get { return "Movie"; }
        }
        /// <summary>
        /// dashboard editor - return form fields
        /// </summary>
        /// <param name="part"></param>
        /// <param name="shapeHelper"></param>
        /// <returns></returns>
        protected override DriverResult Editor(MoviePart part, dynamic shapeHelper) {
            return ContentShape("Parts_Movie_Edit", () =>
                                                    shapeHelper.EditorTemplate(TemplateName: "Parts/Movie", Model: part, Prefix: Prefix));
        }
    }
}