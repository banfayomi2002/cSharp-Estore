namespace eStoreDAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model")
        {
        }

        public virtual DbSet<OrderLineitem> OrderLineitems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderLineitem>()
                .Property(e => e.SellingPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderLineitem>()
                .Property(e => e.Timer)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.Timer)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderLineitems)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.CostPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.MSRP)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Timer)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderLineitems)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}
