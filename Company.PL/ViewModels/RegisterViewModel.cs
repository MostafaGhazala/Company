using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="First Name Is Required")]
		public string FName { set; get; }
		[Required(ErrorMessage = "Last Name Is Required")]
		public string LName { set; get; }
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress]
		public string Email { set; get; }
		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { set; get; }
		[Required(ErrorMessage = "ConfirmPassword Is Required")]
		[Compare("Password",ErrorMessage ="Confirm Password Don't Like Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { set; get; }
		public bool IsAgree { set; get; }
	}
}
