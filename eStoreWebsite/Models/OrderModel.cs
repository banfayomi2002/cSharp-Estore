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

         //<summary>
         //Get a list of OrderDetailDTO for every order for user
         //</summary>
         //<returns>List of OrderDetailDTOs</returns>

         public async Task<List<OrderDetailsDTO>> GetAllDetailsForAllOrdersForUserAsync()
         {
             var _svc = new OrderService(new AppDbContext());
             return await _svc.GetAllDetailsForAllOrdersForUserAsync(UserID);
         }

         //<summary>
         //Get a list of Order for every order for user
         //</summary>
         //<returns>List of Order</returns>

           public async Task<List<OrderModel>> GetAllOrderForUser(string userId)
           {
            List<OrderModel> models = new List<OrderModel>();

           ICollection<Order>  allDetails = await _dal.FindAllAsync(o => o.UserID ==userId);
           foreach (Order order in allDetails)
           {
               OrderModel model = new OrderModel();
              
               model.OrderDate = order.OrderDate;
               model.OrderID = order.OrderID;
                   
               models.Add(model);//add to list
           }
           return models;

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