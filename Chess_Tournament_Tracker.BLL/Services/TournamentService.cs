using Chess_Tournament_Tracker.DAL.Repositories;
using Chess_Tournament_Tracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.Services
{
    public class TournamentService : ITournamentService
    {
        ITournamentRepository _Repository;
        public TournamentService(ITournamentRepository tournamentRepository )
        {
            _Repository = tournamentRepository; 
        }

        public Tournament Add(Tournament tournament)
        {
            return _Repository.Insert(tournament);
        }

        public Tournament Add()
        {
            throw new NotImplementedException();
        }

        public void AddPlayer(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            return _Repository.Delete(GetById(id));
        }

        public void DeletePlayer(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tournament> GetAll()
        {
            throw new NotImplementedException();
        }

        public Tournament GetById(Guid id)
        {
            return _Repository.FindOne(id)?? throw new ArgumentNullException("Not Found");
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
