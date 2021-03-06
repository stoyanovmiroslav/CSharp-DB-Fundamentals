﻿using Microsoft.EntityFrameworkCore;
using ProductShop.Data.EntityConfiguration;
using ProductShop.Models;
using System;

namespace ProductShop.Data
{
    public class ProductShopContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<CategoryProduct>(new CategoryProductConfig());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfig());
        }
    }
}