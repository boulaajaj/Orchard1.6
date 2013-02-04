using Orchard.ContentManagement;

namespace RichSite.Models {
    public class RichDependencyPart:ContentPart<RichDependencyPartRecord> {
        public string Name { get; set; }
    }
}

