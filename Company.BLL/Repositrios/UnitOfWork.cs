using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositrios
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext dbContext;

        public IDepartmentRepository DepartmentRepository { get ; set ; }
        public IEmployeeRepostory EmployeeRepostory { get; set ; }
        public UnitOfWork(CompanyDbContext dbContext)
        {
            DepartmentRepository = new DepartmentRepository(dbContext);
            EmployeeRepostory = new EmployeeRepository(dbContext);
            this.dbContext=dbContext;
        }

        public async Task<int> Complete()
        => await dbContext.SaveChangesAsync();
    }
}
