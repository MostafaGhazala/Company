using Company.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Company.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required !!")]
        [MinLength(5, ErrorMessage = "Min Lenght Is 5")]
        [MaxLength(50, ErrorMessage = "Max Lenght Is 50")]
        public string Name { get; set; }
        [Range(20, 35, ErrorMessage = "Age Must Be Between 20 , 35")]
        public int Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
                           , ErrorMessage = "Your Address Must Be Like 123-Street-City-Country")]
        public string Address { set; get; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { set; get; }
        public IFormFile Image { get; set; }
        public string ImageName { set; get; }

        public DateTime HireDate { get; set; }
        
        [ForeignKey("Department")]
        public int? DepartmentId { set; get; }
        public Department Department { get; set; }
    }
}
