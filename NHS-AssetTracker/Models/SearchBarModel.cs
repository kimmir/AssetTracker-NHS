using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_AssetTracker.Models
{
    public class SearchBarModel
    {
        [Required]
        public string SearchType { get; set; }

        [DisplayName("SearchText")]
        [Required]
        public string SearchText { get; set; }

        public string search { get; set; }
    }
}
