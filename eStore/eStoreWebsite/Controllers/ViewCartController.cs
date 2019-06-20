using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Diagnostics;
using eStoreDAL;
using eStoreWebsite.Models;

namespace eStoreWebsite.Controllers
{
    public class ViewCartController : Controller
    {
        // GET: View
        public ActionResult Index()
        {
            return View();
        }

       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> genOrder()
        {
            OrderModel model = new OrderModel();
            model.UserID = Convert.ToString(Session["userid"]);
           
            string retVal = "";
            try
            {
                bool addOK = await model.AddOrderAsync((CartItemDTO[])Session["cart"], (double)Session["orderamt"]);

                if (model.OrderID > 0) //order added
                {
                    retVal = "Order " + model.OrderID + " Created!";
                    if (model.BackOrderFlag > 0)
                    {
                        retVal += "Some goods were backordered!";
                    }
                }
                else//problem
                {
                    retVal = model.Message + ", try again later!";
                }
            }
            catch (Exception ex) //big problem
            {
                retVal = "Order was not created, try again later! - " + ex.Message;
            }
            Session.Remove("cart");
            return Content(retVal);
        }
    }
}