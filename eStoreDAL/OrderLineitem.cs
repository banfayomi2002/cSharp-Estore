
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eStoreDAL
{

    public partial class OrderLineitem
    {
        [Key]
        public int LineItemID { get; set; }

        public int OrderID { get; set; }

        [Required]
        [StringLength(15)]
        public string ProductID { get; set; }

        public int QtyOrdered { get; set; }

        public int QtySold { get; set; }

        public int QtyBackOrdered { get; set; }

        [Column(TypeName = "money")]
        public decimal SellingPrice { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timer { get; set; }

        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }
    }
}
