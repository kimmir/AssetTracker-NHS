using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_AssetTracker.Models
{
    public class TeamModel
    {
        public int TeamID { get; set; } //02.04.20 - Kim: TeamModel
        public int StaffID { get; set; }
        public int AssetID { get; set; }
    }
}
