using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    [Table("ProductPrice_Discount")]
    public partial class ProductPriceDiscount
    {
        [Key]
        [Column("ProductPrice_DiscountID")]
        public int ProductPriceDiscountId { get; set; }
        [Column("ProductPriceID")]
        public int ProductPriceId { get; set; }
        [Column("DiscountID")]
        public int DiscountId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PromoPrice { get; set; }

        [ForeignKey(nameof(DiscountId))]
        [InverseProperty("ProductPriceDiscount")]
        public virtual Discount Discount { get; set; }
        [ForeignKey(nameof(ProductPriceId))]
        [InverseProperty("ProductPriceDiscount")]
        public virtual ProductPrice ProductPrice { get; set; }
    }
}
