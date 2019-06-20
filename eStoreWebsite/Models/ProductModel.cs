using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;


using System.ComponentModel.DataAnnotations;

// added for Service and DTO classes
using eStoreDAL;

namespace eStoreWebsite.Models
{
    public class ProductModel
    {
        private ProductService _dal;
        // Auto Implemented properties
        public string ProdCode { get; set; }
        public string Prodname { get; set; }
        public string Description { get; set; }
        public string Graphic { get; set; }
        public decimal Msrp { get; set; }
        public decimal CostPrice { get; set; }
        public int Qob { get; set; }
        public int Qoh { get; set; }

        //<summary>
        //constructor should pass instantiated DbContext
        //<summary>

        public ProductModel()
        {
            _dal = new ProductService(new AppDbContext());
        }

        //<summary>
        //Abstract a list of ProductModels from data access layer Products
        //</summary>
        //<returns>abstracted list of ProductModels</returns>

        public async Task<List<ProductModel>> GetAllAsync()
        {
            List<ProductModel> models = new List<ProductModel>();
            ICollection<Product> prdsData = await _dal.GetAllAsync();

            foreach (Product prod in prdsData)
            {
                ProductModel model = new ProductModel();
                model.ProdCode = prod.ProductID;
                model.Prodname = prod.ProductName;
                model.Graphic = prod.GraphicName;
                model.CostPrice = prod.CostPrice;
                model.Msrp = prod.MSRP;
                model.Qob = prod.QtyOnBackOrder;
                model.Qoh = prod.QtyOnHand;
                model.Description = prod.Description;
                models.Add(model);//add to list
            }
            return models;

        }
    }
}