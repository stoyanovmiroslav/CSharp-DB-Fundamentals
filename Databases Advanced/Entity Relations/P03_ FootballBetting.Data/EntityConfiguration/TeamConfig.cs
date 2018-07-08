using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasMany(x => x.Players)
                   .WithOne(x => x.Team)
                   .HasForeignKey(x => x.TeamId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.HomeGames)
               .WithOne(x => x.HomeTeam)
               .HasForeignKey(x => x.HomeTeamId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.AwayGames)
               .WithOne(x => x.AwayTeam)
               .HasForeignKey(x => x.AwayTeamId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
