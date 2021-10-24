using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NHS_AssetTracker.Models;

namespace NHS_AssetTracker.Data
{
    /* public class ApplicationDbContext : IdentityDbContext - Original -  Athar 03.04.2020 */
    /* public class ApplicationDbContext : IdentityDbContext<ApplicationUser> - New -  Athar 03.04.2020 */
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
