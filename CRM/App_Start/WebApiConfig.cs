using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CRM
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors(new EnableCorsAttribute(WebConfigurationManager.AppSettings["Cors.Origins"],
                WebConfigurationManager.AppSettings["Cors.Headers"],
                WebConfigurationManager.AppSettings["Cors.Methods"]));
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
