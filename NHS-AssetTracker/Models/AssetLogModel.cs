using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_AssetTracker.Models
{
    public class AssetLogModel
    {
        public String TimeIn { get; set; } //02.04.20 - Kim: AssetLogModel
        public String TimeOut { get; set; }
        public int AssignedStaffID { get; set; }
        public int AssignedLocationID { get; set; }
        public int AssignedAssetID { get; set; }
        public int ATaskID { get; set; }
        public int AssignedTeam { get; set; }        
        public String LastMaintenence { get; set; }
        public String NextMaintenence { get; set; }
        public String LastCleaningDate { get; set; }
        public String NextCleaningDate { get; set; }
    }
}
