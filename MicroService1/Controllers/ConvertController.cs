using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MicroService1.Controllers
{
	public class ConvertController
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

	}
}