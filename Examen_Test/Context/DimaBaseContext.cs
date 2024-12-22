using System;
using System.Collections.Generic;
using Examen_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Examen_Test.Context;

public partial class DimaBaseContext : DbContext
{
    public DimaBaseContext()
    {
    }

    public DimaBaseContext(DbContextOptions<DimaBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Active> Actives { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductSale> ProductSales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=89.110.53.87:5522;Database=dima_base;Username=dima;Password=dima");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Active>(entity =>
        {
            entity.HasKey(e => e.ActiveId).HasName("active_pk");

            entity.ToTable("Active", "Examen");

            entity.Property(e => e.ActiveId)
                .ValueGeneratedNever()
                .HasColumnName("Active_ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("manufacturer_pk");

            entity.ToTable("Manufacturer", "Examen");

            entity.Property(e => e.ManufacturerId)
                .ValueGeneratedNever()
                .HasColumnName("Manufacturer_ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("product_pk");

            entity.ToTable("Product", "Examen");

            entity.Property(e => e.ProductId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("Product_ID");
            entity.Property(e => e.Cost).HasPrecision(19, 4);
            entity.Property(e => e.MainImagePath).HasMaxLength(1000);
            entity.Property(e => e.ManufacturerId).HasColumnName("Manufacturer_ID");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.IsActiveNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IsActive)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_active_fk");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("product_manufacturer_fk");
        });

        modelBuilder.Entity<ProductSale>(entity =>
        {
            entity.HasKey(e => e.ProductSaleId).HasName("productsale_pk");

            entity.ToTable("ProductSale", "Examen");

            entity.Property(e => e.ProductSaleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ProductSale_ID");
            entity.Property(e => e.ClientServiceId).HasColumnName("ClientService_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSales)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productsale_product_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
