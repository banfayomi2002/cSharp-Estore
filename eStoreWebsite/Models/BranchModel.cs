using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//added for Branch service
using eStoreDAL;

//added for task
using System.Threading.Tasks;

namespace eStoreWebsite.Models
{
    //BranchModel
    public class BranchModel
    {
        private BranchService _dal;
        /// <summary>
        /// constructor - sends instantiated DbContext for eStoreContext
        /// </summary>
        public BranchModel()
        {
            _dal = new BranchService(new AppDbContext());
        }

        /// <summary>
        /// Get 3 closet branches by address
        /// </summary>
        /// <returns>List of 3 BranchModels</returns>
        public async Task<List<BranchDetailsDTO>> GetThreeClosetBranchesAsync(float lat, float lng)
        {
            var _svc = new BranchService(new AppDbContext());
            return await _svc.GetClosetBranchesAsync(lat, lng);
        }
    }
}