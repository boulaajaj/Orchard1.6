using System.Collections.Generic;
using Orchard.ContentManagement.Records;

namespace Pluralsight.Movies.Models {
    public class MoviePartRecord:ContentPartRecord {
        public MoviePartRecord() {
            MovieActors=new List<MovieActorRecord>();
        }
// ReSharper disable InconsistentNaming
        public virtual string IMDB_ID { get; set; }
// ReSharper restore InconsistentNaming
        public virtual int YearReleased { get; set; }
        public virtual MPAARating Rating { get; set; }
        public virtual string Tagline { get; set; }
        public virtual string Keywords { get; set; }
       
        public virtual IList<MovieActorRecord> MovieActors { get; set; }
    }
}