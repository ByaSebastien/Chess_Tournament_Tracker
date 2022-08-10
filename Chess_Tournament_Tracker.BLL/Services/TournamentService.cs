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
        private ITournamentRepository _tournamentRepository;
        private IUserRepository _userRepository;
        public TournamentService(ITournamentRepository tournamentRepository, IUserRepository userRepository)
        {
            _tournamentRepository = tournamentRepository;
            _userRepository = userRepository;
        }

        public Tournament Add(Tournament tournament)
        {
            return _tournamentRepository.Insert(tournament);
        }

        public void AddPlayer(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            return _tournamentRepository.Delete(GetById(id));
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
            return _tournamentRepository.FindOne(id) ?? throw new ArgumentNullException("Not Found");
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
