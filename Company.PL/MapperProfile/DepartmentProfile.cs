using AutoMapper;
using Company.DAL.Models;
using Company.PL.ViewModels;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Company.PL.MapperProfile
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel,Department>().ReverseMap();
        }
    }
}
