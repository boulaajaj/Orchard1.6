using System.Web.Routing;
using Orchard;
using Orchard.Security;

namespace Pluralsight.Movies.Dependency
{
    public class DependencyResolverMovies
    {
        public  static T Resolve<T>(System.Web.HttpContextBase httpContext)
        {
            var requestContext = new RequestContext(httpContext, RouteTable.Routes.GetRouteData(httpContext));
            return requestContext.GetWorkContext().Resolve<T>();
        }
    }
}