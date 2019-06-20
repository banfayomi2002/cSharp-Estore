using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using eStoreWebsite.Models;

namespace eStoreWebsite.Controllers
{
    public class HomeController : Controller
    {

        //Get: Register

        public ActionResult Register()
        {
            ViewBag.Message = "Please fill in all fields!";
            return View();
        }
        // GET: Home
        public ActionResult Index()
        {
            //just put out a couple of cosmetics on the home page if they are not lggged in

         if(Session["loginstatus"] == null)
           {
             Session["loginstatus"]= "not logged in";
            }
 
          if((string)Session["loginstatus"]=="not logged in")
              {
                  Session["message"] = "most functionality requires you to login!";
               }
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserModel model)
        {
            ActionResult response = null;
            try
            {
                string newid = await model.Register();
                Guid guidOut;

                if (Guid.TryParse(newid, out guidOut))
                {
                    response = Content("Customer Registered!-Proceed to login");
                }
                else
                {
                    response = Content("Customer not registered! - " + newid); //newid = error
                }
            }
            catch (Exception ex)
            {
                response = Content("Customer not registered! - Error " + ex.Message);
            }
            return response;
        }
    }
}