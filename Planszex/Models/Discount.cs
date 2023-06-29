using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class Discount
    {
        public Discount()
        {
            ProductPriceDiscount = new HashSet<ProductPriceDiscount>();
        }

        [Key]
        [Column("DiscountID")]
        public int DiscountId { get; set; }
        [Column("UserGroupID")]
        public int UserGroupId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int Amount { get; set; }
        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [ForeignKey(nameof(UserGroupId))]
        [InverseProperty("Discount")]
        public virtual UserGroup UserGroup { get; set; }
        [InverseProperty("Discount")]
        public virtual ICollection<ProductPriceDiscount> ProductPriceDiscount { get; set; }
    }
}
