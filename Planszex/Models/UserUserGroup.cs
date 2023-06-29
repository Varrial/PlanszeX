using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    [Table("User_UserGroup")]
    public partial class UserUserGroup
    {
        [Key]
        [Column("User_UserGroupID")]
        public int UserUserGroupId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("UserGroupID")]
        public int UserGroupId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserUserGroup")]
        public virtual User User { get; set; }
        [ForeignKey(nameof(UserGroupId))]
        [InverseProperty("UserUserGroup")]
        public virtual UserGroup UserGroup { get; set; }
    }
}
