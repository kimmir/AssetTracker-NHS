using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NHS_AssetTracker.Models
{
    public class StaffModel //23.03.20 - Kim: Staff Model
    {
        //Data fields for registration form - Matches the ERD staff members table
        //Constraints are subject to change
        [Display(Name = "NHS ID Number")]
        [Range(100000,999999, ErrorMessage = "You need to enter a valid NHS ID")]
        public int StaffId { get; set; }
        [Display(Name = "Forename")]
        [Required(ErrorMessage = "Forename is required")]
        public string FirstName { get; set; }
        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Surname is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "A role must be selected")]
        public string Role { get; set; }
        [Required(ErrorMessage = "A team must be selected")]
        public string Team { get; set; }
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }        
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Passwords must be between 10 and 50 characters")]        
        public string password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Passwords do not match")]        
        public string confirmPassword { get; set; }

    }
}
