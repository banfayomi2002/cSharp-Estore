using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//added for task
using System.Threading.Tasks;

//added for model
using eStoreWebsite.Models;

namespace eStoreWebsite.Controllers
{
    public class BranchController : Controller
    {
        // GET: Branch
        public ActionResult Index()
        {
            return View();
        }
    }
}