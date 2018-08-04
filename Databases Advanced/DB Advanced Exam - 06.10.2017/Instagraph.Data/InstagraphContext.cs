using Instagraph.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagraph.Data
{
    public class InstagraphContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserFollower> UserFollowers { get; set; }

        public DbSet<UserFollower> UsersFollowers { get; set; }

        public InstagraphContext() { }

        public InstagraphContext(DbContextOptions options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFollower>(e =>
            {
                e.HasKey(k => new { k.FollowerId, k.UserId });

                e.HasOne(u => u.Follower)
                 .WithMany(u => u.UsersFollowing)
                 .HasForeignKey(u => u.FollowerId)
                 .OnDelete(DeleteBehavior.Restrict);
                
               e.HasOne(u => u.User)
                .WithMany(u => u.Followers)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasMany(u => u.Posts)
                  .WithOne(u => u.User)
                  .HasForeignKey(u => u.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

                e.HasMany(u => u.Comments)
                 .WithOne(u => u.User)
                 .HasForeignKey(u => u.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(u => u.ProfilePicture)
                 .WithMany(u => u.Users)
                 .HasForeignKey(u => u.ProfilePictureId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}