using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BloodBank.Authorization
{
    public enum UserType
    {
        Admin,
        Donor
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeUser : AuthorizeAttribute
    {
        public UserType UserType { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = (HttpContext)httpContext.Session["CurrentUser"];
            var typeString = httpContext.Session["UserType"];
            if (typeString != null)
            {
                UserType type = (UserType)Enum.Parse(typeof(UserType), typeString.ToString(), true);
                if (type == UserType)
                    return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            if(UserType == UserType.Admin)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Admin", action = "Login", area = "" }));
            else
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Donors", action = "Login", area = "" }));
        }
    }
}