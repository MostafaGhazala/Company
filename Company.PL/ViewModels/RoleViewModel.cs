using Microsoft.AspNetCore.Mvc;
using System;

namespace Company.PL.ViewModels
{
    public class RoleViewModel 
    {
     public string Id { set; get; }
        public string RoleName { set; get; }
        public RoleViewModel()
        {
            Id=Guid.NewGuid().ToString();
        }

    }
}
