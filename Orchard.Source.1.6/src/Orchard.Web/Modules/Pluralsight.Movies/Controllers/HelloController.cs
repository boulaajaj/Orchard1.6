using System.Web.Http;
using Orchard;
using Orchard.Localization;

namespace Pluralsight.Movies.Controllers {
    public class HelloController : ApiController
    {

        public HelloController(
            IOrchardServices orchardServices)
        {
            Services = orchardServices;
            T = NullLocalizer.Instance;
        }

        public IOrchardServices Services { get; private set; }
        public Localizer T { get; set; }

        public string GetAnyting()
        {
            return "Hello, world!";
        }

        public CMSContent GetTest(int id)
        {
            return new CMSContent(){Name = "Hello, TEST!"}; 
        }
    }

    public class CMSContent
    {
        public string Name { get; set; }
    }
}