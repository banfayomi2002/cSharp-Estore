using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Transactions;

namespace eStoreDAL
{
    public class OrderService : BaseService<Order>
    {
        public OrderService(AppDbContext ctx) : base(ctx) { }

        //<param name ='custid">customerid</param>
        //<returns>
        public async Task<List<OrderDetailsDTO>> GetAllDetailsForAllOrdersForUserAsync(string uid)
        {
            List<OrderDetailsDTO> allDetails = new List<OrderDetailsDTO>();

            var results = from o in _context.Set<Order>()
                          join l in _context.Set<OrderLineitem>() on o.OrderID equals l.OrderID
                          join p in _context.Set<Product>() on l.ProductID equals p.ProductID
                          where (o.UserID == uid)
                          select new OrderDetailsDTO
                          {
                              OrderAmount = o.OrderAmount,
                              OrderID = o.OrderID,
                              ProductName = p.ProductName,
                              SellingPrice = l.SellingPrice,
                              QtySold = l.QtySold,
                              QtyOrdered = l.QtyOrdered,
                              QtyBackOrdered = l.QtyBackOrdered,
                              OrderDate = o.OrderDate
                          };
            allDetails = await results.ToListAsync<OrderDetailsDTO>();
            return allDetails;

        }
        
        public async Task<Dictionary<string, Object>> AddOrderAsync(int[] qty, string[] prodcd,
                                                                     Decimal[] sellPrice, string uid,
                                                                      Decimal oTotal)
        {
            var boFlg = false;
            Dictionary<string, Object> dictionaryReturnValues = new Dictionary<string, Object>();

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Order myOrder = new Order();

                myOrder.OrderDate = DateTime.Now;
                myOrder.OrderAmount = oTotal;
                myOrder.UserID = uid;
                myOrder = await AddAsync(myOrder);
                for (var idx = 0; idx < qty.Length; idx++)
                {
                    if (qty[idx] > 0)
                    {
                        OrderLineitem item = new OrderLineitem();
                        var pcd = prodcd[idx];
                        ProductService prodSvc = new ProductService(_context);
                        item.Product = await prodSvc.FindAsync(p => p.ProductID == pcd);

                        if (item.Product.QtyOnHand > qty[idx])
                        {
                            item.Product.QtyOnHand = item.Product.QtyOnHand - qty[idx];
                            item.QtySold = qty[idx];
                            item.QtyOrdered = qty[idx];
                            item.QtyBackOrdered = 0;
                            item.SellingPrice = sellPrice[idx];
                        }
                        else //not enough stock
                        {
                            item.QtyBackOrdered = qty[idx] - item.Product.QtyOnHand;
                            item.Product.QtyOnBackOrder = item.Product.QtyOnBackOrder +
                                                           (qty[idx] - item.Product.QtyOnHand);
                            item.Product.QtyOnHand = 0;
                            item.QtyOrdered = qty[idx];
                            item.QtySold = item.QtyOrdered - item.QtyBackOrdered;
                            item.SellingPrice = sellPrice[idx];
                            boFlg = true;
                        }
                        myOrder.OrderLineitems.Add(item);
                    }
                }
                myOrder = await UpdateAsync(myOrder, myOrder.OrderID);

                transaction.Complete();

                dictionaryReturnValues.Add("orderid", myOrder.OrderID);
                dictionaryReturnValues.Add("boflag", boFlg);
                return dictionaryReturnValues;

            }


        }

       


    }

}
