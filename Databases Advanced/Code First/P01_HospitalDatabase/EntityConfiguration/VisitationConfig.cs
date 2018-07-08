using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.EntityConfiguration
{
    class VisitationConfig : IEntityTypeConfiguration<Visitation>
    {
        public void Configure(EntityTypeBuilder<Visitation> builder)
        {
            builder.HasKey(p => p.VisitationId);

            builder.Property(p => p.Comments)
                   .HasMaxLength(250)
                   .IsUnicode();
        }
    }
}
