﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; set; }
        IEmployeeRepostory EmployeeRepostory { get; set; }

        Task<int> Complete();
    }
}
