using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_AssetTracker.Models
{
    /* Extended this class to IdentityUser for using the Staff Role value - Athar 03.04.2020 */
    public class ApplicationUser : IdentityUser
    {
        public string StaffRole { get; set; }
    }
}
