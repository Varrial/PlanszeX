using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class StoreData
    {
        [Column("StoreDataID")]
        public int StoreDataId { get; set; }
        [Required]
        [StringLength(30)]
        public string AttribiuteName { get; set; }
        [Required]
        [StringLength(30)]
        public string AttribiuteValue { get; set; }
    }
}
