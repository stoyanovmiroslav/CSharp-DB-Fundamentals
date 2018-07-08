using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.EntityConfiguration
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x => x.DoctorId);

            builder.Property(p => p.Name)
                   .HasMaxLength(100)
                   .IsUnicode();

            builder.Property(p => p.Specialty)
                   .HasMaxLength(100)
                   .IsUnicode();

        }
    }
}
