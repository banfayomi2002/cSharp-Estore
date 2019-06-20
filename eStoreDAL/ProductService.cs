using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreDAL
{
    public class ProductService : BaseService<Product>
    {
        //<summary
        //The contractor requires an open DbContext to work with
        //</summary>
        //<param name="context">An open DbContext</param>

        public ProductService(AppDbContext ctx) : base(ctx) { }
    }
}
