using BusTicketsSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BusTicketsSystem.Data
{
    public class BusTicketsContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BusStation> BusStations { get; set; }
        public DbSet<BusCompany> BusCompanies { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        public BusTicketsContext()
        {

        }

        public BusTicketsContext(DbContextOptions dbContextOptions)
          : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConfigurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BusStation>().HasMany(x => x.TripsDest)
                                             .WithOne(x => x.DestinationBusStation)
                                             .HasForeignKey(x => x.DestinationBusStationId)
                                             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusStation>().HasMany(x => x.TripsOrigin)
                                             .WithOne(x => x.OriginBusStation)
                                             .HasForeignKey(x => x.OriginBusStationId)
                                             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>().HasMany(x => x.Reviews)
                                           .WithOne(x => x.Customer)
                                           .HasForeignKey(x => x.CustomerId)
                                           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>().HasOne(x => x.Customer)
                                         .WithMany(x => x.Reviews)
                                         .HasForeignKey(x => x.CustomerId)
                                         .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
