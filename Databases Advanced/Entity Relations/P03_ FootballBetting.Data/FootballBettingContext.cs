using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.EntityConfiguration;
using P03_FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {

        }

        public FootballBettingContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Bet> Bets { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Bet>(new BetConfig());
            modelBuilder.ApplyConfiguration<Color>(new ColorConfig());
            modelBuilder.ApplyConfiguration<Country>(new CountryConfig());
            modelBuilder.ApplyConfiguration<Game>(new GameConfig());
            modelBuilder.ApplyConfiguration<Player>(new PlayerConfig());
            modelBuilder.ApplyConfiguration<PlayerStatistic>(new PlayerStatisticConfig());
            modelBuilder.ApplyConfiguration<Position>(new PositionConfig());
            modelBuilder.ApplyConfiguration<Team>(new TeamConfig());
            modelBuilder.ApplyConfiguration<Town>(new TownConfig());
            modelBuilder.ApplyConfiguration<User>(new UserConfig());
        }
    }
}
