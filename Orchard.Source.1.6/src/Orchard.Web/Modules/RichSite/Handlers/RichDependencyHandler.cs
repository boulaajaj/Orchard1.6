using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using RichSite.Models;

namespace RichSite.Handlers {
    public class RichDependencyHandler:ContentHandler {
        public RichDependencyHandler(IRepository<RichDependencyPartRecord> richRepository)
        {
            Filters.Add(StorageFilter.For(richRepository));         
        }
    }
}

