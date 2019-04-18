using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
	public class UserInputViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Слово")]
		public string Word { get; set; }

		[Required]
		[Display(Name = "Действие")]
		public bool IsActionChecked { get; set; }
	}
}