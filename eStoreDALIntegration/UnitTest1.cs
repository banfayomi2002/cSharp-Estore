using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using eStoreDAL;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace eStoreDALIntegration
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task ProductServiceGetAll_ShouldReturnProductCollection()
        {
          ProductService _svc = new ProductService(new AppDbContext());
         ICollection<Product> allProducts = await _svc.GetAllAsync();
         Assert.IsInstanceOfType(allProducts, typeof(ICollection<Product>));

        }
    }
}
