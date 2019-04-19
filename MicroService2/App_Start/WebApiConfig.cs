﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using CustomFormatters;

namespace MicroService2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Конфигурация и службы веб-API

			// Маршруты веб-API
			// Маршруты веб-API
			config.Routes.MapHttpRoute(
				name: "PostRequests",
				routeTemplate: "api/{controller}"
			);

			config.Formatters.Add(new PlainTextMediaTypeFormatter(Encoding.UTF8));
		}
    }
}
