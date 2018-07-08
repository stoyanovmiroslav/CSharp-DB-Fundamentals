using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(e => e.Name)
                       .HasMaxLength(80)
                       .IsUnicode(true);

            builder.Property(e => e.Description)
                        .IsUnicode()
                        .IsRequired(false);

            builder.HasMany(e => e.StudentsEnrolled)
                  .WithOne(e => e.Course)
                  .HasForeignKey(e => e.CourseId);

            builder.HasMany(e => e.HomeworkSubmissions)
                  .WithOne(e => e.Course)
                  .HasForeignKey(e => e.CourseId);

            builder.HasMany(e => e.Resources)
               .WithOne(e => e.Course)
               .HasForeignKey(e => e.CourseId);
        }
    }
}