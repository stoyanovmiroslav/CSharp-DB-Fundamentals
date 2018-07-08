using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                        .Property(x => x.Date)
                        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Customer>()
                        .HasMany(e => e.Sales)
                        .WithOne(e => e.Customer)
                        .HasForeignKey(e => e.CustomerId);

            modelBuilder.Entity<Customer>()
                        .Property(x => x.Name)
                        .HasMaxLength(100)
                        .IsUnicode();

            modelBuilder.Entity<Customer>()
                        .Property(x => x.Email)
                        .HasMaxLength(80)
                        .IsUnicode(false);

            modelBuilder.Entity<Product>()
                        .HasMany(e => e.Sales)
                        .WithOne(e => e.Product)
                        .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Product>()
                        .Property(x => x.Name)
                        .HasMaxLength(50)
                        .IsUnicode();

            modelBuilder.Entity<Product>()
                       .Property(x => x.Description)
                       .HasMaxLength(250)
                       .HasDefaultValue("No description");

            modelBuilder.Entity<Store>()
                        .HasMany(e => e.Sales)
                        .WithOne(e => e.Store)
                        .HasForeignKey(e => e.StoreId);

            modelBuilder.Entity<Store>()
                        .Property(x => x.Name)
                        .HasMaxLength(80)
                        .IsUnicode();
        
        }
    }
}