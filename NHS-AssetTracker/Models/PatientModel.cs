using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_AssetTracker.Models
{
    public class PatientModel
    {
        public int PatientID { get; set; } //02.04.20 - Kim: PatientModel
        public String Forename { get; set; }
        public String Surname { get; set; }
        public String Address { get; set; }
        public int ContactNo { get; set; }
        public string ContactNoString { get; set; }
        public int CarerID { get; set; }
    }
}
