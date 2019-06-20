using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eStoreDAL
{

    public class BranchDetailsDTO
    {
        public int BranchID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Distance { get; set; }
    }
}
