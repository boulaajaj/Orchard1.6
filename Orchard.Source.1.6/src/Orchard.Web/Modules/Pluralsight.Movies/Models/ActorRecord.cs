using System.Collections.Generic;
using Orchard.Data.Conventions;

namespace Pluralsight.Movies.Models {
    public class ActorRecord {
        public ActorRecord() {
            ActorMovies=new List<MovieActorRecord>();
        }
        public virtual int Id { get; set; }
        public virtual string Name{ get; set; }

        [CascadeAllDeleteOrphan]
        public virtual IList<MovieActorRecord> ActorMovies { get; set; }

    }
}