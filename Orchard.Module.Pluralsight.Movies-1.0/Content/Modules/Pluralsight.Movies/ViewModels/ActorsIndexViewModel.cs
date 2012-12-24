using System.Collections.Generic;
using Pluralsight.Movies.Models;

namespace Pluralsight.Movies.ViewModels {
    public class ActorsIndexViewModel {
        public IEnumerable<ActorRecord> Actors { get; set; }
        public dynamic Pager { get; set; }
    }
}