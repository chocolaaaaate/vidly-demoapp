using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes(); // this says get the custom routing information from attributes on methods in controller classes. 

            ////Custom routes - add before the generic default one - order matters. Most specific first, most generic last.
            ////this lets us do something like /movies/released/2015/04 (release by year and month)
            //routes.MapRoute("MoviesByReleaseDate",                  // name of the route
            //                "movies/released/{year}/{month}",       // url of the route 
            //                new { controller = "Movies", action = "ByReleaseDate" },  // the default 
            //                new { year = "\\d{4}", month = "\\d{2}"} // \\d{4} ...\\d says digit, {4} says repeat x 4...so saying 4 digits 
            //                );

            //Generic default route 
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
