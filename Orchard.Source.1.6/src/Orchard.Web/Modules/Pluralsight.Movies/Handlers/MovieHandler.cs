using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Pluralsight.Movies.Models;

namespace Pluralsight.Movies.Handlers {
    public class MovieHandler:ContentHandler {
        public MovieHandler(IRepository<MoviePartRecord> moviePartRepository    )  {
            Filters.Add(StorageFilter.For(moviePartRepository));
        }
    }
}