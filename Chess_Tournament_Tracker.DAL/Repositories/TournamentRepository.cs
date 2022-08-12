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
    public class TournamentRepository : RepositoryBase<Tournament>,ITournamentRepository
    {
        public TournamentRepository(TournamentContext context) : base(context) { }

        public IEnumerable<Tournament> GetLastTenTournamentsInProgressOnDateDescending()
        {
            return Entities.Where(t => t.Status != TournamentStatus.Finished).OrderByDescending(t => t.UpdateDate).Take(10);
        }
        public Tournament? FindOneWithPlayer(Guid id)
        {
            return Entities.Include(t => t.Users).SingleOrDefault(t => t.Id == id);
        }
        public Tournament? FindOneWithPlayer(Func<Tournament, bool> predicate)
        {
            return Entities.Include(t => t.Users).SingleOrDefault(predicate);
        }
    }
}
