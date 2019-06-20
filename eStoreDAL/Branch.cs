using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace eStoreDAL
{
    public partial class Branch
    {
        public int BranchID { get; set; }

        [StringLength(150)]
        public string Street { get; set; }

        [StringLength(150)]
        public string City { get; set; }

        [StringLength(2)]
        public string Region { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }
    }
}
