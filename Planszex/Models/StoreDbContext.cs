using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Planszex.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository
{
    public partial class StoreDbContext : DbContext
    {
        public StoreDbContext()
        {
        }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Description> Description { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<FavouriteProduct> FavouriteProduct { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<ProductPrice> ProductPrice { get; set; }
        public virtual DbSet<ProductPriceDiscount> ProductPriceDiscount { get; set; }
        public virtual DbSet<PromoCode> PromoCode { get; set; }
        public virtual DbSet<StoreData> StoreData { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserUserGroup> UserUserGroup { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<WarehouseProduct> WarehouseProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=STACJONARNY\\SQLEXPRESS01;Initial Catalog=StoreDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Comment1).IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.Property(e => e.DiscountId).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.UserGroup)
                    .WithMany(p => p.Discount)
                    .HasForeignKey(d => d.UserGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Discount_UserGroup");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.Property(e => e.Subject).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmailNavigation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Email_User");
            });

            modelBuilder.Entity<FavouriteProduct>(entity =>
            {
                entity.HasKey(e => e.FavouriteId)
                    .HasName("PK_FavouriteProducts");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.FavouriteProduct)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_FavouriteProduct_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FavouriteProduct)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavouriteProduct_User");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.CompanyName).IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Order");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_User");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.PaymentMethod).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Sku).IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.Description)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.DescriptionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Product_Description");
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Order_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Order_Product");
            });

            modelBuilder.Entity<ProductPrice>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPrice)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductPrice_Product");
            });

            modelBuilder.Entity<ProductPriceDiscount>(entity =>
            {
                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.ProductPriceDiscount)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK_ProductPrice_Discount_Discount");

                entity.HasOne(d => d.ProductPrice)
                    .WithMany(p => p.ProductPriceDiscount)
                    .HasForeignKey(d => d.ProductPriceId)
                    .HasConstraintName("FK_ProductPrice_Discount_ProductPrice");
            });

            modelBuilder.Entity<PromoCode>(entity =>
            {
                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<StoreData>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AttribiuteName).IsUnicode(false);

                entity.Property(e => e.AttribiuteValue).IsUnicode(false);

                entity.Property(e => e.StoreDataId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Login).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.PasswordHash).IsFixedLength();

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.Surname).IsUnicode(false);
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<UserUserGroup>(entity =>
            {
                entity.HasOne(d => d.UserGroup)
                    .WithMany(p => p.UserUserGroup)
                    .HasForeignKey(d => d.UserGroupId)
                    .HasConstraintName("FK_UserGroup_User_UserGroup");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserUserGroup)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_UserGroup_User");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<WarehouseProduct>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WarehouseProduct)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Warehouse_Product_Product");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.WarehouseProduct)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_Warehouse_Product_Warehouse");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public List<Product> GetProducts(int? count)
        {
            if (count != null)
            {
                SqlParameter sqlParameter = new SqlParameter("@count", count);
                //return Database.SqlQuery<Product>("SelectPromoProducts @count", sqlParameter).ToList();
                return Product.FromSqlRaw("SelectPromoProducts @count={0}", sqlParameter).ToListAsync().Result;
            }
            else return Product.FromSqlRaw("SelectAllProducts").ToListAsync().Result;
        }

        //USER
        public User GetUser(String login, string password)
        {
  
            if ( password != null && login != null)
            {
                User user = null;
                user = User.FromSqlRaw("GetUser @login={0}, @password={1}", login, password).ToListAsync().Result.FirstOrDefault();
                return (user != null) ? user : null;
            }
            return null;
        }

        public void CreateUser(User siteUser, string password)
        {
            string login;
            string email;
            string name;
            string surname;
            string phone;
            string address;
            bool? permission;

            if (siteUser != null && password != null)
            {
                login = siteUser.Login;
                email = siteUser.Email;
                phone = siteUser.Phone;
                name = siteUser.Name;
                surname = siteUser.Surname;
                address = siteUser.Address;
                permission = null;

                Database.ExecuteSqlRaw("CreateUser @login={0}, @password={1}, @email={2} ,@name={3},@surname={4},@address={5},@phone={6}, @permission={7}", login, password, email,name,surname,address,phone,permission);
            }
        }

        //STORE

        public int GetCounter()
        {
            SqlParameter ret = new SqlParameter();
            ret.ParameterName = "@counter";
            ret.SqlDbType = System.Data.SqlDbType.VarChar;
            ret.Size = 30;
            ret.Direction = System.Data.ParameterDirection.Output;
            Database.ExecuteSqlRaw("GetCounter @counter={0} OUTPUT", ret);
            return int.Parse(ret.Value.ToString());
        }

        public void IncrementCounter()
        {
            int counter = GetCounter() + 1;
            Database.ExecuteSqlRaw("UpdateCounter @counter={0}", counter.ToString());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
