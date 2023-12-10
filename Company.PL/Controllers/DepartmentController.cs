using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositrios;
using Company.DAL.Models;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork=unitOfWork;
            this.mapper=mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {//GetAll
            ///
            IEnumerable<Department> departments;
            if (string.IsNullOrEmpty(SearchValue))
                departments = await unitOfWork.DepartmentRepository.GetAll();
            else
                departments=unitOfWork.DepartmentRepository.GetDepartmentByName(SearchValue);

            
            ///
            
            var MappedDep = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(MappedDep);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if(ModelState.IsValid)
            {
                var MappedDep=mapper.Map<DepartmentViewModel, Department>(departmentVM);
                await unitOfWork.DepartmentRepository.Add(MappedDep);
                int result =await unitOfWork.Complete();
                if (result>0)
                {
                    TempData["Message"]="Department Is Created";
                }
                return RedirectToAction("Index");
            }
            return View(departmentVM);
        }
        public async Task<IActionResult> Details(int? id, string viewName="Details") 
        {
        if(id is  null)
            {
                return BadRequest();
            }
            var department =await unitOfWork.DepartmentRepository.Get(id.Value);
            var MappedDep = mapper.Map<Department, DepartmentViewModel>(department);
            if (department is null)
                return NotFound();
            return View(viewName,MappedDep);
        }
        public async  Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
            //if (id is null)
            //{
            //    return BadRequest();
            //}
            //var department = departmentRepository.Get(id.Value);
            //if (department is null)
            //    return NotFound();
            //return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id,DepartmentViewModel departmentVM)
        {
            if(id != departmentVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedDep = mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    unitOfWork.DepartmentRepository.Update(MappedDep);
                    await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    throw;
                }
            }
            return View(departmentVM);
        }
        //public IActionResult Delete(Department department)
        //{
        //    if (department is null)
        //    {
        //        return BadRequest();
        //    }
        //    else { departmentRepository.Delete(department); }
        //    if (department is null)
        //        return NotFound();
        //    return RedirectToAction(nameof(Index));

        //}
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int id,DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
            {
                return BadRequest();
            }
            try
            {
                var MappedDep = mapper.Map<DepartmentViewModel, Department>(departmentVM);

                unitOfWork.DepartmentRepository.Delete(MappedDep);
                await unitOfWork.Complete();
                return RedirectToAction((nameof(Index)));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return View(departmentVM);

            }
        }



    }
    }
