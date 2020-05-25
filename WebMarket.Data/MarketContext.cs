using System;
using Microsoft.EntityFrameworkCore;
using WebMarket.Model.Data;

namespace WebMarket.Data
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {
        }

        public DbSet<Order> Order { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<OrderProduct> OrderProduct { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>().Property(op => op.Status).HasConversion(c => c.ToString(), c => (OrderStatus)Enum.Parse(typeof(OrderStatus), c));
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<OrderProduct>().ToTable("OrderProduct").HasKey(op => new { op.OrderId, op.ProductId });
            modelBuilder.Entity<OrderProduct>().HasOne(o => o.Order).WithMany(op => op.OrderProduct).IsRequired().HasForeignKey(op => op.OrderId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrderProduct>().HasOne(p => p.Product).WithMany(op => op.OrderProduct).IsRequired().HasForeignKey(op => op.ProductId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory").HasKey(pc => new { pc.ProductId, pc.CategoryId });
            modelBuilder.Entity<ProductCategory>().HasOne(p => p.Product).WithMany(pc => pc.ProductCategory).HasForeignKey(pc => pc.ProductId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductCategory>().HasOne(p => p.Category).WithMany(pc => pc.ProductCategory).HasForeignKey(pc => pc.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }    
}
