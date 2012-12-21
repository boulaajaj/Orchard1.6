using Orchard.ContentManagement;

namespace Pluralsight.Movies.Models {
    public class MoviePart:ContentPart<MoviePartRecord> {
        public string IMDB_Id {
            get { return Record.IMDB_ID; }
            set { Record.IMDB_ID = value; }
        }

        public int YearReleased
        {
            get { return Record.YearReleased; }
            set { Record.YearReleased = value; }
        }

        public MPAARating Rating
        {
            get { return Record.Rating; }
            set { Record.Rating = value; }
        }
    }
}