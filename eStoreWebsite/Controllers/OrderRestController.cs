using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using eStoreWebsite.Models;
using eStoreDAL;

namespace eStoreWebsite.Controllers
{
    /// <summary>
    /// Author: Evan Lauersen
    /// Date: Created: Feb 27, 2015
    /// Purpose: Web API based controller returning JSON array of all order details for current user
    /// Revisions: none
    /// </summary>
    public class OrdersRESTController : ApiController
    {
        [Route("api/orders/{id}")]
        public async Task<IHttpActionResult> Get(string id)
        {
            OrderModel model = new OrderModel();
            model.UserID = id;
            List<OrderDetailsDTO> orders = await model.GetAllDetailsForAllOrdersForUserAsync();
            return Ok(orders); // http 200
        }

       
    }
}

