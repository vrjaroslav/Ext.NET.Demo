using System.ComponentModel.DataAnnotations;
using Ext.Net.MVC;

namespace Uber.Web.Models
{
	public class LoginModel
	{
		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
        [Field(LabelSeparator="")]
		public bool RememberMe { get; set; }
	}
}
