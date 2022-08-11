using Chess_Tournament_Tracker.BLL.DTO.Tournament;
using Chess_Tournament_Tracker.BLL.Exceptions;
using Chess_Tournament_Tracker.BLL.Mappers;
using Chess_Tournament_Tracker.DAL.Repositories;
using Chess_Tournament_Tracker.Models.Entities;
using Chess_Tournament_Tracker.Models.Enums;
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

        public Tournament Insert(FormTournamentDTO insertTournament)
        {
            Tournament tournament = insertTournament.ToDAL();
            tournament.Id = Guid.NewGuid();
            tournament.CreationDate = DateTime.Now;
            return _tournamentRepository.Insert(tournament);

        }

        public void AddPlayer(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            Tournament? tournament = _tournamentRepository.FindOne(id);

            if (tournament == null)
                throw new KeyNotFoundException("Doesn't exist");
            if (tournament.Status == TournamentStatus.InProgress)
                throw new TournamentRulesException("Cannot delete a tournament in progress");

            return _tournamentRepository.Delete(tournament);

        }

        public void DeletePlayer(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tournament> GetAll()
        {

            return _tournamentRepository.FindMany();
        }

        public Tournament GetById(Guid id)
        {
            return _tournamentRepository.FindOne(id) ?? throw new ArgumentNullException("Not Found");
        }

        public bool Update(FormTournamentDTO updateTournament, Guid id)
        {


            Tournament? tournament = _tournamentRepository.FindOne(id);
            if (tournament is null)
                throw new KeyNotFoundException("Doesn't exist");
            if (updateTournament.Status == TournamentStatus.InProgress)
                throw new TournamentRulesException("Cannot update a tournament in progress");

            tournament = updateTournament.ToDAL(tournament);
            return _tournamentRepository.Update(tournament);
        }
    }
}
