using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Encuesta", action = "Registrar", id = UrlParameter.Optional }
            );
            */

            routes.MapRoute(
            name: "Encuesta",
            url: "Encuesta/Responder/{enlace}",
            defaults: new { controller = "Encuesta", action = "Responder" }
            );

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Inicio", action = "Login", id = UrlParameter.Optional }
            );

        }
    }
}
