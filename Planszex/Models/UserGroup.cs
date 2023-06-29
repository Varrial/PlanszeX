using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            Discount = new HashSet<Discount>();
            UserUserGroup = new HashSet<UserUserGroup>();
        }

        [Key]
        [Column("UserGroupID")]
        public int UserGroupId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("UserGroup")]
        public virtual ICollection<Discount> Discount { get; set; }
        [InverseProperty("UserGroup")]
        public virtual ICollection<UserUserGroup> UserUserGroup { get; set; }
    }
}
