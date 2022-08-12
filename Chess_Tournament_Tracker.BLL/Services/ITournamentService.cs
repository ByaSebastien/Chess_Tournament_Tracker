using Chess_Tournament_Tracker.BLL.DTO.Tournaments;
using Chess_Tournament_Tracker.Models.Entities;

namespace Chess_Tournament_Tracker.BLL.Services
{
    public interface ITournamentService
    {
        Tournament Insert(FormTournamentDTO tournament);
        bool Delete(Guid id);
        bool Update(FormTournamentDTO updateTournament, Guid id);
        void RegisterPlayerInTournament(Guid tournamentId, Guid UserId);
        void UnregisterPlayerInTournament(Guid tournamentId, Guid UserId);
        IEnumerable<LastTenTournamentsInProgressOnDateDescendingDTO> GetLastTenTournamentsInProgressOnDateDescending();
        Tournament GetById(Guid id);
    }
}