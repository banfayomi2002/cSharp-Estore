using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStoreWebsite.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Diagnostics;


namespace eStoreWebsite.Controllers
{
    public class LoginController : Controller
    {
       private IAuthenticationManager AuthenticationManager; 
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Login
        public ActionResult Home()
        {
            return View();
        }

        //post login/login

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync(UserModel model){
        ActionResult response = null; //pass success msg or error string back to the ajax form
    try{
        AuthenticationManager = Request.GetOwinContext().Authentication;
        bool loginOk = await model.LoginAsync(AuthenticationManager);

if(loginOk)
{
   Session["userid"] = model.UserID;
   Session["message"] ="Welcome "+model.UserName;
   Session["loginstatus"] = "logged in as "+model.UserName;
   response = Content("Success");
}
else
  {
 response = Content("login failed - try again");
   }
}
catch(InvalidOperationException ex)
   {
 if(ex.InnerException != null){ Debug.WriteLine("Error - "+ex.InnerException.Message);}
   else{Debug.WriteLine("Error -"+ex.Message);}
   response = Content("A major error has occured - contact tech support - tech@mystore.com");
   }
   return response;
       }

        //Post:/Logout/Login
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Logout()
        {
            Session.Abandon();
            IAuthenticationManager mgr = Request.GetOwinContext().Authentication;
            mgr.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index","Home");
        }
    }
}