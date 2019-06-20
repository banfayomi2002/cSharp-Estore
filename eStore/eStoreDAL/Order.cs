using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace eStoreDAL
{
    

    public partial class Order
    {
        public Order()
        {
            OrderLineitems = new HashSet<OrderLineitem>();
        }

        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "money")]
        public decimal OrderAmount { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timer { get; set; }

        public virtual ICollection<OrderLineitem> OrderLineitems { get; set; }
    }
}
