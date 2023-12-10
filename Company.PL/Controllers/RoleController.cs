using AutoMapper;
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
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            this.roleManager=roleManager;
            this.mapper=mapper;
        }
        public async Task<IActionResult> Index(string name)
        {

            if (string.IsNullOrEmpty(name))
            {
                var Roles = await roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id=R.Id,
                    RoleName=R.Name
                    

                }).ToListAsync();
                return View(Roles);
            }
            else
            {
                var Role = await roleManager.FindByNameAsync(name);
                var MappedRole = new RoleViewModel()
                {
                    Id=Role.Id,
                    RoleName = Role.Name
                   
                };
                return View(new List<RoleViewModel> { MappedRole });
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel RoleVM)
        {
            if (ModelState.IsValid)
            {
                var MappedEmp = mapper.Map<RoleViewModel, IdentityRole>(RoleVM);
                await roleManager.CreateAsync(MappedEmp);

               
                
                return RedirectToAction("Index");
            }
            return View(RoleVM);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }
            var Role = await roleManager.FindByIdAsync(id);
            if (Role is null)
                return NotFound();
            var MappedUser = mapper.Map<IdentityRole, RoleViewModel>(Role);

            return View(viewName, MappedUser);
        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel roleVM)
        {

            if (id != roleVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await roleManager.FindByIdAsync(id);
                    role.Name = roleVM.RoleName;
                    
                    await roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    throw;
                }
            }
            return View(roleVM);
        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel roleVM)
        {
            if (id != roleVM.Id)
            {
                return BadRequest();
            }
            try
            {
                var user = await roleManager.FindByIdAsync(id);
                await roleManager.DeleteAsync(user);
                return RedirectToAction((nameof(Index)));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(roleVM);

            }
        }


    }
}
