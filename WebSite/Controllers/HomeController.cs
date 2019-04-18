using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace WebSite.Controllers
{
	public class HomeController : Controller
	{
		public string MicroService1Address
		{
			get
			{
				return WebConfigurationManager.AppSettings["MicroService1Address"];
			}
		}

		public string MicroService2Address
		{
			get
			{
				return WebConfigurationManager.AppSettings["MicroService2Address"];
			}
		}

		public ActionResult Index()
		{
			return View();
		}

	}
}