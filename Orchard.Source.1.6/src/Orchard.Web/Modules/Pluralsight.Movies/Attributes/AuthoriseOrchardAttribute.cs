using System.Web;
using System.Web.Mvc;
using Orchard.Security;
using Orchard.Security.Permissions;
using Pluralsight.Movies.Dependency;

namespace Pluralsight.Movies.Attributes {
    public class AuthoriseOrchardAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        private readonly PermissionEnum _permissions;

        public AuthoriseOrchardAttribute(PermissionEnum permissions)
        {
            _permissions = permissions;
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext) {
            var authorizer = DependencyResolverMovies.Resolve<IAuthorizer>(httpContext);

            switch(_permissions) {
                case PermissionEnum.LookupMovie:
                    return authorizer.Authorize(Permissions.LookupMovie); 
                default:
                    throw  new HttpException("Permission not recognised");
            }
                      
        }
    }
}