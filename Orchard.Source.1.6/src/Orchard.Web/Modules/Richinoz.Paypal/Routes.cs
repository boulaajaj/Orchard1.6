using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace Richinoz.Paypal
{
    public class Routes : IRouteProvider
    {
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
	            new RouteDescriptor {
		            Route = new Route(
			            "Paypal/PostToPaypal",
			            new RouteValueDictionary {
				            {"area", "Richinoz.Paypal"},
				            {"controller", "Paypal"},
				            {"action", "PostToPaypal"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "Richinoz.Paypal"}
			            },
			            new MvcRouteHandler())
	            },
                 new RouteDescriptor {
		            Route = new Route(
			            "Paypal/IPN",
			            new RouteValueDictionary {
				            {"area", "Richinoz.Paypal"},
				            {"controller", "Paypal"},
				            {"action", "IPN"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "Richinoz.Paypal"}
			            },
			            new MvcRouteHandler())
	            }
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var route in GetRoutes())
            {
                routes.Add(route);
            }
        }
    }
}