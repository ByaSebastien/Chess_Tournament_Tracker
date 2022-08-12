using Chess_Tournament_Tracker.BLL.DTO.Tournaments;
using Chess_Tournament_Tracker.BLL.Exceptions;
using Chess_Tournament_Tracker.BLL.Mappers;
using Chess_Tournament_Tracker.DAL.Repositories;
using Chess_Tournament_Tracker.Models.Entities;
using Chess_Tournament_Tracker.Models.Enums;

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
            if (insertTournament.EndInscription < DateTime.Now.AddDays(insertTournament.MinPlayer))
                throw new TournamentRulesException($"End of inscription must have {insertTournament.MinPlayer} days upper than creation");
            Tournament tournament = insertTournament.ToDAL();
            tournament.Id = Guid.NewGuid();
            tournament.CreationDate = DateTime.Now;
            tournament.UpdateDate = DateTime.Now;
            return _tournamentRepository.Insert(tournament);

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


        public Tournament GetById(Guid id)
        {
            return _tournamentRepository.FindOneWithPlayer(id) ?? throw new KeyNotFoundException("Not Found");
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
        public IEnumerable<LastTenTournamentsInProgressOnDateDescendingDTO> GetLastTenTournamentsInProgressOnDateDescending()
        {
            return _tournamentRepository.GetLastTenTournamentsInProgressOnDateDescending().Select(t =>
              new LastTenTournamentsInProgressOnDateDescendingDTO(t)
               );
        }
        public void RegisterPlayerInTournament(Guid tournamentId, Guid userId)
        {
            Tournament? tournament = _tournamentRepository.FindOneWithPlayer(tournamentId);
            User? user = _userRepository.FindOne(userId);
            if (tournament is null)
                throw new KeyNotFoundException("Tournament doesn't exist");
            if (user is null)
                throw new KeyNotFoundException("Player doesn't exist");

            if (CanRegister(user, tournament))
            {
                tournament.Users.Add(user);                
                _tournamentRepository.Update(tournament);
            }
        }
        public void UnregisterPlayerInTournament(Guid tournamentId, Guid UserId)
        {
            Tournament? tournament = _tournamentRepository.FindOneWithPlayer(tournamentId);
            User? user = _userRepository.FindOne(UserId);
            if (tournament is null)
                throw new KeyNotFoundException("Tournament doesn't exist");
            if (user is null)
                throw new KeyNotFoundException("Player doesn't exist");
            if (tournament.Status != TournamentStatus.WaitingPlayer)
                throw new TournamentRulesException("Tournament has began");
            if (!tournament.Users.Any(p => p.Id == user.Id))
                throw new TournamentRulesException("this player was not register");
            tournament.Users.Remove(user);
            _tournamentRepository.Update(tournament);
        }

        private bool CanRegister(User player, Tournament tournament)
        {
            if (tournament.Status != TournamentStatus.WaitingPlayer)
                throw new TournamentRulesException("Tournament has began");
            if (tournament.EndInscription > DateTime.Now)
                throw new TournamentRulesException("Register closed");
            if (tournament.Users.Any(p => p.Id == player.Id))
                throw new TournamentRulesException("Already register");
            if (tournament.Users.Count >= tournament.MaxPlayer)
                throw new TournamentRulesException("Tournament full");
            if (CheckCategories(tournament, player))
                throw new TournamentRulesException("Can't fit in any category");
            if (CheckELO(tournament, player))
                throw new TournamentRulesException("Cant register cause to ELO");
            if (tournament.IsWomenOnly && player.Gender == UserGender.Male)
                throw new TournamentRulesException("Only women can register");
            return true;
        }

        private static int CalculAge(DateTime endInscription, DateTime birthDate)
        {
            int age = endInscription.Year - birthDate.Year;
            if (birthDate > endInscription.AddYears(-age)) age--;
            return age;
        }

        private static bool CheckCategories(Tournament tournament, User player)
        {
            bool flag = false;
            int age = CalculAge(tournament.EndInscription, player.Birthate);
            if (tournament.Category.HasFlag(TournamentCategory.Junior) && age < 18)
                flag = true;
            if (tournament.Category.HasFlag(TournamentCategory.Senior) && age >= 18 && age < 60)
                flag = true;
            if (tournament.Category.HasFlag(TournamentCategory.Veteran) && age >= 60)
                flag = true;
            return flag;
        }

        public static bool CheckELO(Tournament tournament, User player)
        {
            if (tournament.MinELO is not null && player.ELO < tournament.MinELO)
                return false;
            if (tournament.MaxELO is not null && player.ELO > tournament.MaxELO)
                return false;
            return true;
        }

    }
}
