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
    public class ShopController : Controller
    {
        // GET: Shop
        // default method will load session variable for shopping cart

        public async Task<ActionResult> Index()
        {
            //only build the catalogue once
            if (Session["cart"] == null)
            {
                try
                {
                    ProductModel model = new ProductModel();
                    List<ProductModel> models = await model.GetAllAsync();
                    if (models.Count() > 0)
                    {
                        CartItemDTO[] myCart = new CartItemDTO[models.Count];//array
                        var ctr = 0;
                        //build CartItem array from List contents
                        foreach (ProductModel m in models)
                        {
                            CartItemDTO item = new CartItemDTO();
                            item.ProductCode = m.ProdCode;
                            item.ProductName = m.Prodname;
                            item.Graphic = m.Graphic;
                            item.Msrp = m.Msrp;
                            item.Description = m.Description;
                            item.Qty = 0;
                            myCart[ctr++] = item;
                        }
                        Session["cart"] = myCart; //load to session
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Catalogue Problem - " + ex.Message;
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(int qty, String detailsProductcode)
        { 
          CartItemDTO[] cart =(CartItemDTO[])Session["cart"];
           String retMsg="";
          
           foreach(CartItemDTO item in cart)
           {
             if(item.ProductCode == detailsProductcode)
             {
               if(qty >0)
               {
                 item.Qty = qty;
                 retMsg = qty + " - item(s) Added!";
                 }
               else
               {
                   item.Qty = 0;
                   retMsg = "- item removed!";
               }
                break;
                }
             }
              Session["cart"] = cart;//store updates back in session
              return Content(retMsg); //send the string back to the client's javascript
           }


    }
}