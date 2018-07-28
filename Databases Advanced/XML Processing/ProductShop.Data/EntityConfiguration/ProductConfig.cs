using Microsoft.EntityFrameworkCore;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.Data.EntityConfiguration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(x => x.Buyer)
                   .WithMany(x => x.ProductsBuyer)
                   .HasForeignKey(x => x.BuyerId);

            builder.HasOne(x => x.Seller)
                   .WithMany(x => x.ProductsSeler)
                   .HasForeignKey(x => x.SellerId);
        }
    }
}
