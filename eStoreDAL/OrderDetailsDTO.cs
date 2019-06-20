using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreDAL
{
    public class OrderDetailsDTO
    {
        public decimal OrderAmount { get; set; }
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public decimal SellingPrice { get; set; }
        public int QtySold { get; set; }
        public int QtyOrdered { get; set; }
        public int QtyBackOrdered { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
