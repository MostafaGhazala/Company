using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositrios
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepostory
    {
        private readonly CompanyDbContext dbContext;

        public EmployeeRepository(CompanyDbContext dbContext) : base(dbContext)
        {
            this.dbContext=dbContext;
        }

        public IQueryable<Employee> GetEmployeesByName(string employeeName)
        {
            return dbContext.Set<Employee>().Where(E=>E.Name.ToLower().Contains(employeeName.ToLower()));
        }
    }
}
