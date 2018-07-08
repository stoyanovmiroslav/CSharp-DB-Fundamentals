using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.EntityConfiguration;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions dbContextOptions) 
            : base(dbContextOptions)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<StudentCourse>(new StudentCourseConfig());
            modelBuilder.ApplyConfiguration<Homework>(new HomeworkConfig());
            modelBuilder.ApplyConfiguration<Course>(new CourseConfig());
            modelBuilder.ApplyConfiguration<Resource>(new ResourceConfig());
            modelBuilder.ApplyConfiguration<Student>(new StudentConfig());
        }
    }
}