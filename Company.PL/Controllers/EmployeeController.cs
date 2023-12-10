using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositrios;
using Company.DAL.Models;
using Company.PL.Helper;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork=unitOfWork;
            this.mapper=mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            #region DataBinding
            //Data Binding 
            //1)ViewData 
            //Transefer Data From Controller[Action] to View 
            //released .Net Framework 3.5
            // ViewData["Message"] ="Hello from View Data";
            //2)ViewBag
            //Transefer Data From Controller[Action] to View 
            //released .Net Framework 4.0
            // ViewBag.Message ="Hello From Bag View"; 
            #endregion
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
                employees =await unitOfWork.EmployeeRepostory.GetAll();
            else
                employees=unitOfWork.EmployeeRepostory.GetEmployeesByName(SearchValue);

            var MappedEmps=mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmps);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            //Mapping 
            #region ManualMapping
            //1.ManualMapping
            //var MappedEmolyee = new Employee()
            //{
            //    Name=employeeVM.Name,
            //    Age=employeeVM.Age,
            //    Address=employeeVM.Address,
            //    Salary=employeeVM.Salary,
            //    IsActive=employeeVM.IsActive,
            //    Email=employeeVM.Email,
            //    PhoneNumber=employeeVM.PhoneNumber,
            //    HireDate=employeeVM.HireDate,

            //}; 
            #endregion

            //2.Auto Mapper
            //This Is Pacakage AutoMapper
            if (ModelState.IsValid)
            {
                var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                if (employeeVM.ImageName is not null)
                {
                    MappedEmp.ImageName=DocumentSettings.UploadImage(employeeVM.Image, "images");

                }

                await unitOfWork.EmployeeRepostory.Add(MappedEmp);
                int result = await unitOfWork.Complete();
                if (result>0)
                {
                    TempData["Message"]="Employee Is Added";
                }
                return RedirectToAction("Index");
            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            

            if (id is null)
            {
                return BadRequest();
            }
            var Employee = await unitOfWork.EmployeeRepostory.Get(id.Value);
            if (Employee is null)
                return NotFound();
            var MappedEmp=mapper.Map<Employee,EmployeeViewModel>(Employee);
           
            return View(viewName, MappedEmp);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments =await unitOfWork.DepartmentRepository.GetAll();
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
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employeeVM)
        {

            if (id != employeeVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                    if (MappedEmp.ImageName is null && employeeVM.Image is not null)
                    {
                        MappedEmp.ImageName= DocumentSettings.UploadImage(employeeVM.Image, "images");
                        unitOfWork.EmployeeRepostory.Update(MappedEmp);

                    }
                    else if (employeeVM.ImageName is not null && employeeVM.Image is not null)
                    {
                        DocumentSettings.DeleteImage(employeeVM.ImageName, "images");
                        MappedEmp.ImageName= DocumentSettings.UploadImage(employeeVM.Image, "images");
                        unitOfWork.EmployeeRepostory.Update(MappedEmp);

                    }
                    else
                    unitOfWork.EmployeeRepostory.Update(MappedEmp);
                    await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    throw;
                }
            }
            return View(employeeVM);
        }
        #region Delete
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
        #endregion
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
            {
                return BadRequest();
            }
            try
            {
                var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                
                unitOfWork.EmployeeRepostory.Delete(MappedEmp);
                var result =await unitOfWork.Complete();
                if (result>0 && employeeVM.ImageName is not null)
                {
                   
                        DocumentSettings.DeleteImage(employeeVM.ImageName, "images");
                    
                }
                return RedirectToAction((nameof(Index)));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);

            }
        }
    }
}
