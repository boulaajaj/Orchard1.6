using System.Collections.Generic;

namespace Pluralsight.Movies.ViewModels {
    public class MovieLookupViewModel {
        public MovieLookupViewModel() {
            MovieResults = new List<MovieResult>();
        }

        public string MovieTitle { get; set; }
        public string IMDB_Id { get; set; }
        public IEnumerable<MovieResult> MovieResults { get; set; }
        public bool NoMatch { get; set; }
    }
}