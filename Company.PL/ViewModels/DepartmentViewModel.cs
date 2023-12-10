using Company.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Company.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }//=>By convention P.k,Identity
        [Required(ErrorMessage = "Code Is Required !!")]//using Data Annotation
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Is Required !!")]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> Employees { set; get; } = new HashSet<Employee>();
    }
}
