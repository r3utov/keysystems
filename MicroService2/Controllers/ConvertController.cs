using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroService2.Controllers
{
	public class ConvertController
	{

		/// -------------------------------------------------------------- 
		/// Обработка текста
		/// --------------------------------------------------------------

		/// <summary>
		/// Инвертирует строку
		/// </summary>
		protected string ConvertText(string input)
		{
			return String.Join("", input.Reverse());
		}
	}
}