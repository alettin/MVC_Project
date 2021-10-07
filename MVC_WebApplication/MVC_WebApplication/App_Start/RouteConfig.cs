using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_WebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Articles",
                url: "article",
                defaults: new { controller = "Article", action = "Index" },
                namespaces: new[] { "MVC_WebApplication.Controllers" }
                );

            routes.MapRoute(
                name: "Events",
                url: "event",
                defaults: new { controller = "Event", action = "Index",id = 99 },
                namespaces: new[] { "MVC_WebApplication.Controllers" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapMvcAttributeRoutes();
        }
    }
}
