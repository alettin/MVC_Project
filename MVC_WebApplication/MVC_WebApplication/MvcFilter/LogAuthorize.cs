using MVC_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebApplication.MvcFilter
{
    public class LogAuthorize : AuthorizeAttribute
    {
        private readonly string _menuCode = "";
        public LogAuthorize()
        {

        }
        public LogAuthorize( string menuCode)
        {
            _menuCode = menuCode;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorize = false;
            //check its roles with menuCode from database or session for action/controller. if it is ok then set isAuthorize = true

            if (isAuthorize)
            {
                return true;
            }
            else
            {
                return false;
            }

            //return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //save unauthorizedrequest to database , 401

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        success = false,
                        message = "You are not authorized to do this action. ",
                        Error = "NotAuthorized"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("~/Error/Unauthorized");
                }
                else
                {
                    //todo login page...
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}