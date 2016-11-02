using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace SealPhoneAngular2
{
    [AttributeUsage(AttributeTargets.Class |
    AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class FilterAuth : ActionFilterAttribute
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();

            if (!controllerName.Equals("Home"))
            {
                if (!controllerName.Equals("Auth") && !actionName.Equals("Login"))
                {
                    if (HttpContext.Current.Session["user"] == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(
                           new RouteValueDictionary{
                                { "controller", "Auth" },
                                { "action", "Login" }
                           });
                        filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}