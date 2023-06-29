using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    [Table("Warehouse_Product")]
    public partial class WarehouseProduct
    {
        [Key]
        [Column("Warehouse_ProductID")]
        public int WarehouseProductId { get; set; }
        [Column("WarehouseID")]
        public int WarehouseId { get; set; }
        [Column("ProductID")]
        public int ProductId { get; set; }
        public int Qty { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("WarehouseProduct")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(WarehouseId))]
        [InverseProperty("WarehouseProduct")]
        public virtual Warehouse Warehouse { get; set; }
    }
}
