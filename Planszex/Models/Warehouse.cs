using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            WarehouseProduct = new HashSet<WarehouseProduct>();
        }

        [Key]
        [Column("WarehouseID")]
        public int WarehouseId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [InverseProperty("Warehouse")]
        public virtual ICollection<WarehouseProduct> WarehouseProduct { get; set; }
    }
}
