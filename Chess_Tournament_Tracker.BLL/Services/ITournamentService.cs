using Chess_Tournament_Tracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.Services
{
    public interface ITournamentService
    {
        Tournament Add();
        bool Delete(Guid id);
        bool Update();
        Tournament GetById (Guid id);
        IEnumerable<Tournament> GetAll();
        void AddPlayer(Guid UserId);
        void DeletePlayer(Guid UserId);
        

    }
}
