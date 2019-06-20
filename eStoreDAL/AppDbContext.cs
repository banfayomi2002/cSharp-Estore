using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eStoreDAL
{
    /// <summary>
    ///     Author:     Evan Lauersen
    ///     Date:       Created: Feb 27, 2015
    ///     Purpose:    Context class to handle db work with identity authentication
    ///     Revisions:  none    
    /// </summary>
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext() : base("eStoreDB") { }
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
        public virtual DbSet<OrderLineitem> OrderLineitems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
