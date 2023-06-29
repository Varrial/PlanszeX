using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class ProductPrice
    {
        public ProductPrice()
        {
            ProductPriceDiscount = new HashSet<ProductPriceDiscount>();
        }

        [Key]
        [Column("ProductPriceID")]
        public int ProductPriceId { get; set; }
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductPrice")]
        public virtual Product Product { get; set; }
        [InverseProperty("ProductPrice")]
        public virtual ICollection<ProductPriceDiscount> ProductPriceDiscount { get; set; }
    }
}
