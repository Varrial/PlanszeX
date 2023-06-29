using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class Comment
    {
        [Key]
        [Column("CommentID")]
        public int CommentId { get; set; }
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Required]
        [Column("Comment")]
        [StringLength(2000)]
        public string Comment1 { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Comment")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Comment")]
        public virtual User User { get; set; }
    }
}
