using FastFood.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Data
{
    public class FastFoodDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Position> Positions { get; set; }

        public FastFoodDbContext()
        {
        }

        public FastFoodDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderItem>(e =>
            {
                e.HasKey(k => new { k.ItemId, k.OrderId });
            });

            builder.Entity<Item>(e =>
            {
                e.HasIndex(p => p.Name).IsUnique();
            });

            builder.Entity<Position>(e =>
            {
                e.HasIndex(p => p.Name).IsUnique();
            });

            builder.Entity<Order>(e =>
            {
                e.Property(p => p.Type).HasDefaultValue(OrderType.ForHere);
            });
        }
    }
}