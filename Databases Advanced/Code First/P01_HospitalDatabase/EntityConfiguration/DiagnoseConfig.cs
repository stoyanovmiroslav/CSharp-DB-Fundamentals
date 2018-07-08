using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.EntityConfiguration
{
    class DiagnoseConfig : IEntityTypeConfiguration<Diagnose>
    {
        public void Configure(EntityTypeBuilder<Diagnose> builder)
        {
            builder.HasKey(e => e.DiagnoseId);

            builder.Property(p => p.Name)
                   .HasMaxLength(50)
                   .IsUnicode();

            builder.Property(p => p.Comments)
                   .HasMaxLength(250)
                   .IsUnicode();
        }
    }
}