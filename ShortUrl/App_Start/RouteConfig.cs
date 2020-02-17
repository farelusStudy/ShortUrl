using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShortUrl
{
    public class RouteConfig
    {
        public static void RegisterRoutes( System.Web.Routing.RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default2",
                url: "KEK{code}",
                defaults: new { controller = "Home", action = "HandleShort", code = UrlParameter.Optional });
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
