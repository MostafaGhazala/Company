using AutoMapper;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Company.PL.MapperProfile
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel,IdentityRole>()
                .ForMember(c=>c.Name,k=>k.MapFrom(r=>r.RoleName))
                .ReverseMap();
        }
    }
}
