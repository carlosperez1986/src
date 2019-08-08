using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Modular.Modules.ModuleASP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "ModuleASP",
            //    url: "ModuleASP/{action}/{id}",
            //    defaults: new { controller = "ModuleASP", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
