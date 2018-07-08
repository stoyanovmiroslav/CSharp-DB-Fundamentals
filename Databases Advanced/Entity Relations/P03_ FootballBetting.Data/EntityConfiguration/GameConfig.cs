using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasMany(x => x.Bets)
                   .WithOne(x => x.Game)
                   .HasForeignKey(x => x.GameId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.PlayerStatistics)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
