using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeRepostory:IGenericRepository<Employee>
    {
        public IQueryable<Employee> GetEmployeesByName (string employeeName);
    }
    
}
