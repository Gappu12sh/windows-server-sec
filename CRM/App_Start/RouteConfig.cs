﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          //  routes.MapRoute(
          //    "User_default",
          //    "User/{controller}/{action}/{id}",
          //    new { action = "Index", id = UrlParameter.Optional },
          //    namespaces: new[] { "CRM.Areas.User.Controllers" }
          //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default1", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "CRM.Controllers" }
            );

           
        }
    }
}