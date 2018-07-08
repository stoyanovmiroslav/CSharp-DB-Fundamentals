using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data.EntityConfiguration
{
    public class ColorConfig : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasMany(x => x.PrimaryKitTeams)
                   .WithOne(x => x.PrimaryKitColor)
                   .HasForeignKey(x => x.PrimaryKitColorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.SecondaryKitTeams)
                   .WithOne(x => x.SecondaryKitColor)
                   .HasForeignKey(x => x.SecondaryKitColorId)
                   .OnDelete(DeleteBehavior.Restrict);

            //Introducing FOREIGN KEY constraint 'FK_Team_Color_SecondaryKitColorId' on table 'Team' may cause cycles or multiple cascade paths.Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
        }
    }
}
