using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StoreApp.DataAccess.Models
{
    public partial class NewDataBaseContext : DbContext
    {
        public NewDataBaseContext()
        {
        }

        public NewDataBaseContext(DbContextOptions<NewDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<OrderLine> OrderLine { get; set; }
        public virtual DbSet<Orders> OrderHistory { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => new { e.LocationId, e.ProductId });

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.LocationId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK_Orders");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.OrderNavigation)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Orders_OrderHistory_OrderID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderLine)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Orders_Product_ProductID");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.OrderHistory)
                    .HasForeignKey(d => d.LocationId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Price).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
