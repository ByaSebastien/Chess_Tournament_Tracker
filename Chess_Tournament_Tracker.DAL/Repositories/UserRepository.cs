using Chess_Tournament_Tracker.DAL.Contexts;
using Chess_Tournament_Tracker.Models.Entities;
using Chess_Tournament_Tracker.Models.Enums;
using Chess_Tournament_Tracker.Tools.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TournamentContext context) : base(context) { }

        public User? FindWithTournament(Guid id)
        {
            return Entities.Include(t => t.Tournaments).SingleOrDefault(u => u.Id == id);
        }

        public bool HasTournamentInProgress(Guid id)
        {
            return Entities.Include(t => t.Tournaments).Any(u => u.Id == id && u.Tournaments.Any( t => t.Status == TournamentStatus.InProgress));
        }
    }
}
