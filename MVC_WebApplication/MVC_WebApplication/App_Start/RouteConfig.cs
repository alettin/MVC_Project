using MVC_WebApplication.Models;
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
                defaults: new { controller = "Event", action = "Index", id = 99 },
                namespaces: new[] { "MVC_WebApplication.Controllers" }
                );

            routes.GenericRoute("GenericUrl",
                "{generic_se_name}",
                new { controller = "Error", action = "PageNotFound" },
                null,
                new[] { "MVC_WebApplication.Controllers" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapMvcAttributeRoutes();
        }
    }

    public static class GenericRouteClass
    {
        public static Route GenericRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            var route = new GenericPathRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens["Namespaces"] = namespaces;
            }

            routes.Add(name, route);

            return route;
        }
    }


    public partial class GenericPathRoute : Route
    {
        public GenericPathRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler)
        {
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData data = base.GetRouteData(httpContext);
            if (data != null)
            {
                var slug = data.Values["generic_se_name"] as string;

                //when you use database you can check this way
                /*
                var urlRecordService = DependencyResolver.Current.GetService<bllUrlRecord>();
                var urlRecord = urlRecordService.CheckUrlRecord(slug);
                */

                //This is for static variables
                UrlRecord urlRecord = new UrlRecord();
                if (slug == "how-to-use-generic-seo-url-in-mvc")
                {
                    urlRecord.SystemTable = SystemTable.Article;
                    urlRecord.SeoUrl = slug;
                    urlRecord.ItemId = 1;
                }
                if (slug == "mvc-event-detail")
                {
                    urlRecord.SystemTable = SystemTable.Event;
                    urlRecord.SeoUrl = slug;
                    urlRecord.ItemId = 1;
                }
                

                if (urlRecord == null)
                {
                    data.Values["controller"] = "Error";
                    data.Values["action"] = "PageNotFound";
                    return data;
                }

                switch (urlRecord.SystemTable)
                {
                    case SystemTable.Article:
                        {
                            data.Values["controller"] = "Article";
                            data.Values["action"] = "ArticleDetails";
                            data.Values["ItemId"] = urlRecord.ItemId;
                            data.Values["SeName"] = urlRecord.SeoUrl;
                        }
                        break;
                    case SystemTable.Event:
                        {
                            data.Values["controller"] = "Event";
                            data.Values["action"] = "EventDetails";
                            data.Values["ItemId"] = urlRecord.ItemId;
                            data.Values["SeName"] = urlRecord.SeoUrl;
                        }
                        break;
                }
            }
            return data;
        }
    }

}