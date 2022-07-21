using MVC_WebApplication.Controllers;
using MVC_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_WebApplication.MvcFilter
{
    public class LogException : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            var exception = filterContext.Exception;
            var httpContext = HttpContext.Current;


            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Index";
            routeData.Values["errorType"] = (int)ExceptionCode.ViewError; //this is your error code. Can this be retrieved from your error controller instead?
            routeData.Values["exception"] = exception;
            routeData.Values["httpContext"] = httpContext;

            using (Controller controller = new ErrorController())
            {
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }

            //base.OnException(filterContext);
        }
    }
}