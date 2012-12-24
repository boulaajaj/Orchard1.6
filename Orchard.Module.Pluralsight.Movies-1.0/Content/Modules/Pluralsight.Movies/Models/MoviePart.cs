using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;

namespace Pluralsight.Movies.Models {
    public class MoviePart : ContentPart<MoviePartRecord> {
        public string IMDB_Id {
            get { return Record.IMDB_Id; }
            set { Record.IMDB_Id = value; }
        }

        public int YearReleased {
            get { return Record.YearReleased; }
            set { Record.YearReleased = value; }
        }

        public MPAARating Rating {
            get { return Record.Rating; }
            set { Record.Rating = value; }
        }

        public string Tagline {
            get { return Record.Tagline; }
            set { Record.Tagline = value; }
        }

        public string Keywords {
            get { return Record.Keywords; }
            set { Record.Keywords = value; }
        }

        public IEnumerable<ActorRecord> Cast {
            get { return Record.MovieActors.ToList().Select(c => c.ActorRecord); }
        }
    }
}