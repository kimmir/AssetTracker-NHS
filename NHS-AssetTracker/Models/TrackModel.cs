using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_AssetTracker.Models
{
    public class TrackModel
    {
        // Added the following data to show asset details in track page. - Athar 29.04.2020
        public int AssetID { get; set; }

        public string AssetType { get; set; }
        public string AssetHome { get;  set; }
        
        public string AssetUser { get; set; }

        public string AssetValue { get; set; }

        public string AssetUsable { get; set; }

        public string AssetNotes { get; set; }

        //Location Data used in location model but needed here to load objects in track page - - Athar 29.04.2020
        public string LocationID { get; set; }
        public string LocationName { get; set; }
        public string LocationPostCode { get; set; }
        public string LocationHead { get; set; }

        // Patiend Data used in Patient model but needed here to load objects in track page - - Athar 29.04.2020

        public string PatientID { get; set; } //02.04.20 - Kim: PatientModel
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string CarerID { get; set; }
    }
}
