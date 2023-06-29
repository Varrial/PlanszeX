using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class Invoice
    {
        [Key]
        [Column("InvoiceID")]
        public int InvoiceId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("OrderID")]
        public int OrderId { get; set; }
        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("Invoice")]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Invoice")]
        public virtual User User { get; set; }
    }
}
