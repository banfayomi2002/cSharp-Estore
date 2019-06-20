using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eStoreWebsite.Models;
using System.Web.Mvc;
using System.Text;

namespace eStoreWebsite.Helpers
{
    public static class CatalogueHelpers
    {
        public static HtmlString Catalogue(this HtmlHelper helper, string id)
        {
            // create tag builder
            var builder = new TagBuilder("ul");
            StringBuilder innerHtml = new StringBuilder();

            // create a valid id
            builder.GenerateId(id);

            // render tag
            if (HttpContext.Current.Session["cart"] != null) // havnt been to database yet
            {
                CartItemDTO[] cart = (CartItemDTO[])HttpContext.Current.Session["cart"];

                foreach (CartItemDTO item in cart)
                {

                    innerHtml.Append("<div class='col-lg-3 col-md-4 col-sm-4 col-xs-12'><li><h3 id='Name" + item.ProductCode + "'>" + item.ProductName + "</h3>");
                    innerHtml.Append("<div><img class='img' alt='' src='/Images/" + item.Graphic + "' id='Graphic"+item.ProductCode +"' "+ "width='256' height='423' />"+ "</div>");
                    innerHtml.Append("<div class='info'>");
                    innerHtml.Append("<p id='Descr" + item.ProductCode + "'data-description='" + item.Description + "'>");
                    innerHtml.Append("<p>"+ item.Description.Substring(0,20) + "...</p>");
                    innerHtml.Append("<div class='price'><span class='st'> Our Price:</span>");
                    innerHtml.Append("<strong id='Price" + item.ProductCode + "'>" + String.Format("{0:C}", Convert.ToDecimal(item.Msrp)) + "</strong></div>");
                    innerHtml.Append("<div class='actions'>");
                    innerHtml.Append("<a href='#details_popup' data-toggle='modal' class='btn btn-danger' data-prodcd='" + item.ProductCode + "'>Details</a></div></div></li></div>");
                }
            }
            builder.InnerHtml = innerHtml.ToString();
            return new HtmlString(builder.ToString());
        }
    }
}