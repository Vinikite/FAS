using Ninject.Planning.Targets;
using System;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;

namespace FAS.WebUI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ClaimPermissionAttribute : ActionFilterAttribute
    {
        public ClaimPermissionAttribute(Target target, Permission permission)
        {
            Permission = permission;
            Target = target;
            UnlessRole = String.Empty;
        }

        public Permission Permission { get; private set; }
        public Target Target { get; private set; }
        public string UnlessRole { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userContext = filterContext.HttpContext.User;

            if (userContext.Identity.IsAuthenticated && !userContext.IsInRole(UnlessRole))
            {
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)userContext.Identity;

                if (!claimsIdentity.Claims.HasAccess(this.Target, this.Permission))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Access" } });
                }
            }
        }
    }
}