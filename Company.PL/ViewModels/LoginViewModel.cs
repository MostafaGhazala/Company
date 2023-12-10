using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress]
		public string Email { set; get; }
		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { set; get; }

		public bool RememberMe { get; set; }
	}
}
