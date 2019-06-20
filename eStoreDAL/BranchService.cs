using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;

namespace eStoreDAL
{
   
    public class BranchService : BaseService<Branch>{
/// <summary>
/// The constructor requires an open DbContext to work with
/// </summary>
/// <param name="context">An open DbContext</param>
public BranchService(AppDbContext ctx) : base(ctx) {}

/// <summary>
/// Get Closest Branches
/// </summary>
/// param name="dictionaryAddresses">lat, lng coordinates determined by google</param>
/// <returns>List of details of 3 closet branches</returns>
public async Task<List<BranchDetailsDTO>> GetClosetBranchesAsync(float? lat, float? lng)
{
List<BranchDetailsDTO> branchDetails = new List<BranchDetailsDTO>();
var latParam = new SqlParameter("@lat", lat);
var lngParam = new SqlParameter("@lng", lng);
var query = _context.Database.SqlQuery<BranchDetailsDTO>(
		"pGetThreeClosestBranches @lat, @lng", latParam, lngParam);
branchDetails = await query.ToListAsync();
return branchDetails;
}
}
}
