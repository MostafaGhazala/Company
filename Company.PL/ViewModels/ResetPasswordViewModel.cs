using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "New Password Is Required")]
		[DataType(DataType.Password)]
		public string NewPassword { set; get; }
		[Required(ErrorMessage = "ConfirmPassword Is Required")]
		[Compare("NewPassword", ErrorMessage = "Confirm Password Don't Like Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { set; get; }
	}
}
