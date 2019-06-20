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
    public class OrderController : Controller
    {
        // GET: Order
       
        public async Task<ActionResult> Index()
 {
            OrderModel model = new OrderModel();
            List<OrderModel> models = await model.GetAllOrderForUser((string)Session["userid"]);
            return View(models);
 
            
        }
    }
}