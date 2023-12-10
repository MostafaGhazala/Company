using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class ForgetPasswordViewModel
	{

		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress]
		public string Email { set; get; }
	}
}
