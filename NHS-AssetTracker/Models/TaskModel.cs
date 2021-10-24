using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_AssetTracker.Models
{
    public class TaskModel
    {
        public int TaskID { get; set; } //02.04/20 - Kim: TaskModel
        public String StartDate { get; set; }
        public String FinishDate { get; set; }
        public int PatientID { get; set; }

    }
}
