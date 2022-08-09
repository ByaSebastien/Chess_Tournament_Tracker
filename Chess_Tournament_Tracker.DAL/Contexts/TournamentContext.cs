using Chess_Tournament_Tracker.DAL.Configurations;
using Chess_Tournament_Tracker.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.DAL.Contexts
{
    public class TournamentContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Tournament> Tournaments => Set<Tournament>();
        public TournamentContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new TournamentConfiguration());
            builder.ApplyConfiguration(new GameConfiguration());
        }                
    }
}
