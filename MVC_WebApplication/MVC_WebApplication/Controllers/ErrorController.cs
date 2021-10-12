using MVC_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebApplication.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(HttpContext httpContext, Exception exception, int errorType = (int)ExceptionCode.Default)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            //SaveErrorMessage(httpContext, exception, methodName, errorType);
            return View();
        }

        private int GetStatusCode(Exception exception)
        {
            var httpException = exception as HttpException;
            return httpException != null ? httpException.GetHttpCode() : (int)HttpStatusCode.InternalServerError;
        }

        public ActionResult Unauthorized()
        {
            ViewBag.PageName = "Unauthorized Page";
            return View();
        }

        public ActionResult PageNotFound()
        {
            var _httpContext = System.Web.HttpContext.Current;
            var _exception = new Exception("Page not found");
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            //SaveErrorMessage(_httpContext, _exception, methodName, 404);
            return View();
        }
    }
}