using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(e => e.Name)
                        .HasMaxLength(100)
                        .IsUnicode();

            builder.Property(e => e.PhoneNumber)
                        .HasColumnType("char(10)")
                        .IsRequired(false)
                        .IsUnicode(false);

            builder.Property(e => e.Birthday)
                        .IsRequired(false);

            builder.HasMany(e => e.CourseEnrollments)
                   .WithOne(e => e.Student)
                   .HasForeignKey(e => e.StudentId);

            builder.HasMany(e => e.HomeworkSubmissions)
                 .WithOne(e => e.Student)
                 .HasForeignKey(e => e.StudentId);
        }
    }
}