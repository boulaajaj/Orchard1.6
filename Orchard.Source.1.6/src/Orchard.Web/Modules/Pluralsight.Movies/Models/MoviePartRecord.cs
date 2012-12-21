using Orchard.ContentManagement.Records;

namespace Pluralsight.Movies.Models {
    public class MoviePartRecord:ContentPartRecord {
// ReSharper disable InconsistentNaming
        public virtual string IMDB_Id { get; set; }
// ReSharper restore InconsistentNaming
        public virtual int YearReleased { get; set; }
        public virtual MPAARating Rating { get; set; }
    }
}