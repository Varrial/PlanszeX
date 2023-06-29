using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class Product
    {
        public Product()
        {
            Comment = new HashSet<Comment>();
            FavouriteProduct = new HashSet<FavouriteProduct>();
            ProductOrder = new HashSet<ProductOrder>();
            ProductPrice = new HashSet<ProductPrice>();
            WarehouseProduct = new HashSet<WarehouseProduct>();
        }

        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Column("CategoryID")]
        public int CategoryId { get; set; }
        [Column("DescriptionID")]
        public int? DescriptionId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? AddDate { get; set; }
        public bool? HavePromoPrice { get; set; }
        public bool? Visible { get; set; }
        [Column("SKU")]
        [StringLength(100)]
        public string Sku { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Product")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(DescriptionId))]
        [InverseProperty("Product")]
        public virtual Description Description { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<Comment> Comment { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<FavouriteProduct> FavouriteProduct { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductPrice> ProductPrice { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<WarehouseProduct> WarehouseProduct { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
