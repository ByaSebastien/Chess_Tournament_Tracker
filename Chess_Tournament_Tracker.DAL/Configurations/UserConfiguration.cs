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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Pseudo).HasMaxLength(255);
            builder.HasIndex(u => u.Pseudo).IsUnique();

            builder.Property(u => u.Mail).HasMaxLength(255);
            builder.HasIndex(u => u.Mail).IsUnique();



        }
    }
}
