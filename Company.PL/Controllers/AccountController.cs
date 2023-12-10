using Company.DAL.Models;
using Company.PL.Helper;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
			this.userManager=userManager;
			this.signInManager=signInManager;
		}
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
            
            if(ModelState.IsValid)//serever side validation
            {
                var user = new ApplicationUser()
                {
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree,
                    Email = model.Email,
                    UserName=model.Email.Split('@')[0],
                };
                var result= await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
			return View(model);
		}

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null) 
                {
                    var flag =await userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index","Home");
                        }
                        
                    }
                    ModelState.AddModelError(string.Empty, "Password Is Invalid");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Is Invalid");
                }

            }
            return View(model);
        }

        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var Token=await userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new {email = user.Email , token = Token},Request.Scheme);
                    var email = new Email()
                    {
                        To=user.Email,
                        Subject="Reset Password",
                        Body = ResetPasswordLink

					};
                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                 ModelState.AddModelError(string.Empty, "Email is not Valid");

            }
            

            return RedirectToAction(nameof(ForgetPassword));
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }
		public IActionResult ResetPassword(string email,string token)
		{
            TempData["email"]=email;
            TempData["token"]=token;
			return View();
		}
        [HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{

            if (ModelState.IsValid)
            {
                string email= TempData["email"] as string;
				string token = TempData["token"] as string;
                var user =await userManager.FindByEmailAsync(email);
                if (user != null) 
                {
                    var result = await userManager.ResetPasswordAsync(user, token, model.NewPassword); 
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Login));
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return RedirectToAction(nameof(BadRequest));

			}
			return View(model);
		}
	}
}
