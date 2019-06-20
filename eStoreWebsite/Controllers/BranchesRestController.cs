using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//added for DTO class
using eStoreDAL;
//added for task
using System.Threading.Tasks;
//added for branch model
using eStoreWebsite.Models;

namespace eStoreWebsite.Controllers
{
    public class BranchesRestController : ApiController
    {
   [Route("api/closebranches/{lat:double}/{lng:double}")]
public async Task<IHttpActionResult> Get(float lat, float lng)
{
BranchModel branch = new BranchModel();
List<BranchDetailsDTO> closeBranches = await branch.GetThreeClosetBranchesAsync(lat, lng);
return Ok(closeBranches); // http 200
    }
  }
}