using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FAS.BLL;

namespace FAS.WebUI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ClaimPermissionAttribute : ActionFilterAttribute
    {
        public const string MethodClaimType = "AccessToMethod";

        public ClaimPermissionAttribute(string permissionName)
        {
            PermissionName = permissionName;
        }

        public string PermissionName { get; private set; }
        public string UnlessRole { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userContext = filterContext.HttpContext.User;

            if (userContext.Identity.IsAuthenticated && !userContext.IsInRole(UnlessRole))
            {
                var userService = DependencyResolver.Current.GetService<IUserService>();
                var user = userService.GetByEmail(userContext.Identity.Name);
                var claims = userService.CreateIdentity(user).Claims.Where(claim => claim.Type.Equals(MethodClaimType));

                if (!claims.Any(claim => claim.Value.Equals(PermissionName)))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Access" } });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}