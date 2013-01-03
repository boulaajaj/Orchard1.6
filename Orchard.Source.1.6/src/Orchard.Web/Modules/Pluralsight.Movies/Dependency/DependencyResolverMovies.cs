using System.Web;
using System.Web.Routing;
using Orchard;
using Orchard.Security;

namespace Pluralsight.Movies.Dependency
{
    public class DependencyResolverMovies
    {
        public  static T Resolve<T>(System.Web.HttpContextBase httpContext) {

            var requestContext = httpContext.Request.RequestContext.GetWorkContext();
            return requestContext.Resolve<T>();
        }
    }
}