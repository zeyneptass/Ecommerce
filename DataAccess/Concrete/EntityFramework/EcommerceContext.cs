using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EcommerceContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ZEYNEP\SQLEXPRESS01;Database=DbEcommerce;Integrated Security=True;TrustServerCertificate=True;");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShippingInfo> ShippingInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Category>()
            //    .HasMany(c => c.Products)
            //    .WithOne(p => p.Category)
            //    .HasForeignKey(p => p.CategoryID);

            //modelBuilder.Entity<Product>()
            //    .HasMany(p => p.ProductImages)
            //    .WithOne(pi => pi.Product)
            //    .HasForeignKey(pi => pi.ProductID);

            //modelBuilder.Entity<Product>()
            //    .HasMany(p => p.OrderItems)
            //    .WithOne(oi => oi.Product)
            //    .HasForeignKey(oi => oi.ProductID);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingInfo)
                .WithOne(si => si.Order)
                .HasForeignKey<ShippingInfo>(si => si.OrderID);

            // HasNoKey metodunu kaldırıyoruz
        }
    }
}

