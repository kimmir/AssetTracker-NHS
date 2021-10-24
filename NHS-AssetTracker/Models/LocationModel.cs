using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_AssetTracker.Models
{
    public class LocationModel
    {
        public int LocationID { get; set; } //02.04.20 - Kim: LocationModel
        public String LocationName { get; set; }
        public String LocationPostCode { get; set; }
        public String LocationHead { get; set; }
    }
}
