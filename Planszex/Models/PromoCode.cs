using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class PromoCode
    {
        [Key]
        [Column("PromoCodeID")]
        public int PromoCodeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int Amount { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        public int Usage { get; set; }
        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
    }
}
