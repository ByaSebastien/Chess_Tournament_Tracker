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
    }
}
