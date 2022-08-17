using Chess_Tournament_Tracker.Models.Entities;
using Chess_Tournament_Tracker.Tools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.DAL.Repositories
{
    public interface ITournamentRepository : IRepository<Tournament>
    {
        IEnumerable<Tournament> GetAllByTen(int offset = 0);
        Tournament? FindOneWithPlayer(Guid id);
        Tournament? FindOneWithPlayer(Func<Tournament,bool> predicate);
        Tournament? FindOneWithGame(Guid id);
        Tournament? FindOneWithGame(Func<Tournament,bool> predicate);
        Tournament? FindDetail(Guid id);
    }
}
