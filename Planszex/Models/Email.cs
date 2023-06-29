using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class Email
    {
        [Key]
        [Column("EmailID")]
        public int EmailId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Subject { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Text { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("EmailNavigation")]
        public virtual User User { get; set; }
    }
}
