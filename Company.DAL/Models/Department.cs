using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }//=>By convention P.k,Identity
        [Required]//using Data Annotation
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> Employees { set; get; } =new HashSet<Employee>();
    }
}
