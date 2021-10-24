using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_AssetTracker.Models
{
    public class StaffMemberModel
    {
        public int StaffID { get; set; } //02.04.20 - Kim: Staffmember model
        public int LocationID { get; set; }
        public String Forename { get; set; }
        public String Surname { get; set; }
        public String Role { get; set; }
        public int SupervisorID { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

    }
}
