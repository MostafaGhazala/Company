using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
	public class ApplicationUser:IdentityUser
	{
		[Required]
		public string FName { set; get; }
		[Required]
		public string LName { set; get; }
		public bool IsAgree { set; get; }
	}
}
