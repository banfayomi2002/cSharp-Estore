namespace eStoreDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public Product()
        {
            OrderLineitems = new HashSet<OrderLineitem>();
        }

        [StringLength(15)]
        public string ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(20)]
        public string GraphicName { get; set; }

        [Column(TypeName = "money")]
        public decimal CostPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal MSRP { get; set; }

        public int QtyOnHand { get; set; }

        public int QtyOnBackOrder { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timer { get; set; }

        public virtual ICollection<OrderLineitem> OrderLineitems { get; set; }
    }
}
