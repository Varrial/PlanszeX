using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class FavouriteProduct
    {
        [Key]
        [Column("FavouriteID")]
        public int FavouriteId { get; set; }
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("FavouriteProduct")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("FavouriteProduct")]
        public virtual User User { get; set; }
    }
}
