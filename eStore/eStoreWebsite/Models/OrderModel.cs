using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;


using System.ComponentModel.DataAnnotations;

// added for Service and DTO classes
using eStoreDAL;
namespace eStoreWebsite.Models
{
    public class OrderModel
    {
        private OrderService _dal;
        public string UserID { get; set; }

        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderAmount { get; set; }

        public int BackOrderFlag { get; set; }

        public string Message { get; set; }

         public OrderModel()
        {
            _dal = new OrderService(new AppDbContext());
        }


        public async Task<bool> AddOrderAsync(CartItemDTO[] items, double amt)
        {
            Dictionary<string, Object> dictionaryReturnValues = new Dictionary<string, Object>();
            BackOrderFlag = 0;
            OrderID = -1;
            var idx = 0;

            //arrays containing pertinent cart field
            var prodcds = new string[items.Length];
            var qty = new int[items.Length];
            var sellPrice = new Decimal[items.Length];
            foreach (CartItemDTO item in items)
            {
                prodcds[idx] = item.ProductCode;
                sellPrice[idx] = item.Msrp;
                qty[idx++] = item.Qty;
            }
            dictionaryReturnValues = await _dal.AddOrderAsync(qty, prodcds, sellPrice, UserID, Convert.ToDecimal(amt));
            OrderID = Convert.ToInt32(dictionaryReturnValues["orderid"]);
            BackOrderFlag = Convert.ToInt32(dictionaryReturnValues["boflag"]);
            if (OrderID > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}