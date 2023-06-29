using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class Description
    {
        public Description()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        [Column("DescriptionID")]
        public int DescriptionId { get; set; }
        [Required]
        [Column("Description", TypeName = "text")]
        public string Description1 { get; set; }

        [InverseProperty("Description")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
