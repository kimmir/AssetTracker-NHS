using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_AssetTracker.Models
{
    public class AssetModel //Model for the asset test
    {
        public int AssetID { get; set; } //02.04.20 - Kim: Asset Model
        public String AssetType { get; set; }
        public int AssetHome { get; set; }
        public int AssetUser { get; set; }
        public String AssetValue { get; set; }
        public int AssetUsable { get; set; }
        public String AssetNotes { get; set; }

        public string AssetLocationName { get; set; } // 29.40.20 - Athar
        public string AssetUsableString { get; set; } // 29.40.20 - Athar

        public string PatientName { get; set; } // 29.40.20 - Athar

        public string AssetLastCleaned { get; set; } // 29.40.20 - Athar

        public string LocationPostCode { get; set; }

        public string LocationHead { get; set; }


        public string PatientID { get; set; } //30.04.20 - Athar
        public String Forename { get; set; } //30.04.20 - Athar
        public String Surname { get; set; } //30.04.20 - Athar
        public String Address { get; set; } //30.04.20 - Athar
        public string ContactNo { get; set; } //30.04.20 - Athar
        public string CarerID { get; set; } //30.04.20 - Athar

        public string search { get; set; } //30.04.20 - Athar
        public string result { get; set; }

    }
}
