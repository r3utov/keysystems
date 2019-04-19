using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using CustomFormatters;

namespace MicroService2
{
    public class RouteConfig
	{
        public static void RegisterRoutes(RouteCollection routes)
        {
			routes.MapRoute(
				name: "Api",
				url: "api",
				defaults: new { controller = "Home", action = "Index" }
			);
			routes.MapRoute(
				name: "Index",
				url: "{controller}/{action}",
				defaults: new { controller = "Home", action = "Index"}
			);
		}
    }
}
