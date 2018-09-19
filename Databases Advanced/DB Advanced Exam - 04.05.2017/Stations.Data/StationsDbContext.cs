using Microsoft.EntityFrameworkCore;
using Stations.Models;

namespace Stations.Data
{
	public class StationsDbContext : DbContext
	{
        public StationsDbContext()
		{
		}

		public StationsDbContext(DbContextOptions options)
			: base(options)
		{
		}
		
	    public DbSet<CustomerCard> Cards { get; set; }

        public DbSet<SeatingClass> SeatingClasses { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Train> Trains { get; set; }

        public DbSet<TrainSeat> TrainSeats { get; set; }

        public DbSet<Trip> Trips { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Trip>(e =>
            {
                e.HasOne(x => x.OriginStation)
                 .WithMany(x => x.TripsFrom)
                 .HasForeignKey(x => x.OriginStationId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.DestinationStation)
                 .WithMany(x => x.TripsTo)
                 .HasForeignKey(x => x.DestinationStationId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SeatingClass>(e =>
            {
                e.HasAlternateKey(x => x.Name);
                e.HasAlternateKey(x => x.Abbreviation);
            });

            modelBuilder.Entity<Station>(e =>
            {
                e.HasAlternateKey(x => x.Name);
            });

            modelBuilder.Entity<Train>(e =>
            {
                e.HasAlternateKey(x => x.TrainNumber);
            });
        }
	}
}