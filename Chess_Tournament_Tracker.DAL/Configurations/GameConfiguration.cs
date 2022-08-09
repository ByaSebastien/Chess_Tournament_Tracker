using Chess_Tournament_Tracker.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.DAL.Configurations
{
    internal class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasOne(g => g.White).WithMany(u => u.GamesAsWhite).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(g => g.Black).WithMany(u => u.GamesAsBlack).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(g => g.CurrentTournament).WithMany(t => t.Games).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
