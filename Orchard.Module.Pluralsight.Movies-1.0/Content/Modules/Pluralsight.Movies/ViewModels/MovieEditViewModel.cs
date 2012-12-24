using System.Collections.Generic;
using Pluralsight.Movies.Models;

namespace Pluralsight.Movies.ViewModels {
    public class MovieEditViewModel {
        public string IMDB_Id { get; set; }
        public int YearReleased { get; set; }
        public MPAARating Rating { get; set; }
        public string Tagline { get; set; }
        public string Keywords { get; set; }
        public IEnumerable<int> Actors { get; set; }
        public IEnumerable<ActorRecord> AllActors { get; set; }  
    }
}