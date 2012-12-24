using Orchard.DisplayManagement.Descriptors;

namespace Pluralsight.Movies {
    public class Shapes : IShapeTableProvider {
        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("Parts_Movie_Tagline")
                .OnDisplaying(displaying =>
                              displaying.ShapeMetadata.Alternates
                              .Add("Parts_Movie_Tagline_" + displaying.ShapeMetadata.DisplayType));
        }
    }
}