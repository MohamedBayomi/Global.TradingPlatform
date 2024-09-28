using EmployeesPortal.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesPortal.Shared.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required, MaxLength(100), MinLength(5)]
        public string Name { get; set; }
        // Add the ResetPasswordCode property
        public string? ResetPasswordCode { get; set; }

        // Add the ResetCodeExpiry property
        public DateTime? ResetCodeExpiry { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
