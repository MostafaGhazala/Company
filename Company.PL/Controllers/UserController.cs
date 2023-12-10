using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositrios;
using Company.DAL.Models;
using Company.PL.Helper;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IMapper mapper)
        {
			this.userManager=userManager;
			this.signInManager=signInManager;
            this.mapper=mapper;
        }
        public async Task<IActionResult>Index(string email)
		{

			if (string.IsNullOrEmpty(email))
			{ 
				var users = await userManager.Users.Select(U => new UserViewModel()
						{
							Id=U.Id,
							Email=U.Email,
							FName=U.FName,
							LName=U.LName,
							Roles=userManager.GetRolesAsync(U).Result,
							PhoneNumber=U.PhoneNumber,

						}).ToListAsync();
				return View(users);
			}
			else
			{
				var user = await userManager.FindByEmailAsync(email);
				var MappedUser = new UserViewModel()
				{
					Id=user.Id,
					Email=user.Email,
					FName =user.FName,
					LName=user.LName,
					PhoneNumber = user.PhoneNumber,
					Roles=userManager.GetRolesAsync(user).Result
				};
				return View (new List<UserViewModel> { MappedUser });
			}
			
				
		}

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            var MappedUser = mapper.Map<ApplicationUser, UserViewModel>(user);
           
            return View(viewName, MappedUser);
        }
        #region Edit
        public async Task<IActionResult> Edit(string id)
        { 
            return await Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel UserVM)
        {

            if (id != UserVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    //var MappedUsere = mapper.Map<UserViewModel, ApplicationUser>(UserVM);
                    var user = await userManager.FindByIdAsync(id);
                   
                    user.FName = UserVM.FName;
                    user.LName = UserVM.LName;
                    user.PhoneNumber = UserVM.PhoneNumber;
                    await userManager.UpdateAsync(user);
                    
                       
                    
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    throw;
                }
            }
            return View(UserVM);
        }



        #endregion
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel userVM)
        {
            if (id != userVM.Id)
            {
                return BadRequest();
            }
            try
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.DeleteAsync(user);
                return RedirectToAction((nameof(Index)));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(userVM);

            }
        }

    }
}
