using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositrios
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly CompanyDbContext dbContext;

        public DepartmentRepository(CompanyDbContext dbContext):base(dbContext)
        {
            this.dbContext=dbContext;
        }

        public IQueryable<Department> GetDepartmentByName(string departmentName)
        {
            
                return dbContext.Set<Department>().Where(E => E.Name.ToLower().Contains(departmentName.ToLower()));
            
        }
    }
}
