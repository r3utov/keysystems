using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using CustomFormatters;
using WebSite.Models;

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


		/// <summary>
		/// Включена ли проверка значения введенного текста на стороне веб-сайта
		/// (перед передачей на сервис)
		/// </summary>
		public bool ServerSideStringValidation { get; set; } = true;

		public ActionResult Index()
		{
			return View();
		}


		/// <summary>
		/// Анализирует данные, полученные из формы, и посылает запрос на выбранный сервис
		/// </summary>
		[HttpPost]
		public ActionResult ProcessText(UserInputViewModel viewModel)
		{
			/// Проверка значения
			if (ServerSideStringValidation == true) {
				if (string.IsNullOrEmpty(viewModel.Word) == true) {
					ModelState.AddModelError(nameof(viewModel.Word), "Задана пустая строка");
					return View("Index", viewModel);
				}
			}
				using (var httpClient = new HttpClient()) {
				/// Выбор сервиса по заданному условию
				string selectedService;
				if (viewModel.IsActionChecked == true) {
					selectedService = MicroService2Address;
				} else {
					selectedService = MicroService1Address;
				}

				try {

					httpClient.BaseAddress = new Uri(selectedService);

					var responseTask = httpClient.PostAsync("convert/",
						new StringContent(
							content: viewModel.Word ?? string.Empty,
							encoding: Encoding.UTF8,
							mediaType: "text/plain"
						));

					responseTask.Wait();
					var result = responseTask.Result;

					var readTask = result.Content.ReadAsAsync<string>(new[] { new PlainTextMediaTypeFormatter(Encoding.UTF8) });
					readTask.Wait();

					if (result.IsSuccessStatusCode == true) {
						ViewBag.Result = readTask.Result;
					} else {
						ViewBag.Result = FormatErrorString(selectedService, $"{result.StatusCode.ToString()} ({readTask.Result})");
					}
				} catch (Exception ex) {
					/// Поиск самого "вложенного" исключения
					while (ex.InnerException != null) {
						ex = ex.InnerException;
					}
					ViewBag.Result = FormatErrorString(selectedService, ex.Message);
				}
			}
			return View();

		}

		private string FormatErrorString(string service, string errorMessage)
		{
			return $"Ошибка при запросе на сервис [{service}]: {Environment.NewLine} {errorMessage}";
		}
	}
}