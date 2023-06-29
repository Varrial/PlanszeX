using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Planszex.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            EmailNavigation = new HashSet<Email>();
            FavouriteProduct = new HashSet<FavouriteProduct>();
            Invoice = new HashSet<Invoice>();
            Order = new HashSet<Order>();
            UserUserGroup = new HashSet<UserUserGroup>();
        }

        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        [StringLength(30)]

        [DisplayName("Login")]
        public string Login { get; set; }
        [Required]
        [MaxLength(32)]
        public byte[] PasswordHash { get; set; }
        public bool? Permission { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "To pole jest wymagane!")]
        [EmailAddress(ErrorMessage = "Fraza nie przypomina adresu email")]

        [DisplayName("Emial")]
        public string Email { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        [StringLength(100)]
        [MinLength(2, ErrorMessage = "Imie jest za krótkie!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        [StringLength(100)]
        [MinLength(2,ErrorMessage ="Nazwisko jest za krótkie!")]

        [DisplayName("Nazwisko")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        [StringLength(100)]
        [MinLength(5, ErrorMessage = "Adres jest za krótki!")]

        [DisplayName("Adres")]
        public string Address { get; set; }
        [StringLength(13)]
        [Required(ErrorMessage = "To pole jest wymagane!")]
        [MinLength(9, ErrorMessage = "Telefon jest za krótki!")]

        [DisplayName("Telefon")]
        public string Phone { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Comment> Comment { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Email> EmailNavigation { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<FavouriteProduct> FavouriteProduct { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Invoice> Invoice { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Order> Order { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserUserGroup> UserUserGroup { get; set; }
    }
}
