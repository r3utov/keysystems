using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace MicroService1.Controllers
{
	public class ConvertController : ApiController
	{

		/// -------------------------------------------------------------- 
		/// Обработка текста
		/// --------------------------------------------------------------

		/// <summary>
		/// Конвертирует текст в base64
		/// </summary>
		protected string ConvertText(string input)
		{
			byte[] inputBytes = Encoding.UTF8.GetBytes(input);
			return Convert.ToBase64String(inputBytes);
		}


		/// -------------------------------------------------------------- 
		/// Методы запросов
		/// --------------------------------------------------------------

		// POST api/<controller>
		public string Post([FromBody]string input)
		{
			/// Проверка строки на стороне сервиса
			if (string.IsNullOrEmpty(input) == true) {
				throw new HttpResponseException(
					new HttpResponseMessage(HttpStatusCode.BadRequest) {
						Content = new StringContent("Передана пустая строка", Encoding.UTF8)
					});
			}
			return ConvertText(input);
		}
	}
}