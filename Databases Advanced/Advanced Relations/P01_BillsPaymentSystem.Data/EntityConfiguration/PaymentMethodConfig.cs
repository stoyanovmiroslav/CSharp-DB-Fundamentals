using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_BillsPaymentSystem.Data.EntityConfiguration
{
    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasIndex(e => new { e.BankAccountId, e.CreditCardId, e.UserId }).IsUnique();

            builder.HasOne(e => e.CreditCard)
                   .WithOne(e => e.PaymentMethod)
                   .HasForeignKey<PaymentMethod>(e => e.CreditCardId);

            builder.HasOne(e => e.BankAccount)
                  .WithOne(e => e.PaymentMethod)
                  .HasForeignKey<PaymentMethod>(e => e.BankAccountId);
        }
    }
}
