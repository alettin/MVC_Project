using MVC_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebApplication.MvcFilter
{
    public class LogAction: ActionFilterAttribute
    {
        private string _description { get; set; }
        private LogType _logType { get; set; }

        public LogAction(string description, LogType logType)
        {
            _description = description;
            _logType = logType;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //filterContext.HttpContext.Request.Url;
            //filterContext.HttpContext.Request.UserHostAddress;
            //filterContext.HttpContext.Request.Url.AbsoluteUri;
            //filterContext.HttpContext.Request.Url.LocalPath;
            //_description
            //_logType

            //save to database

        }
    }
}