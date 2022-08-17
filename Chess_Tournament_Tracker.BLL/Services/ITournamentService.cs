using Chess_Tournament_Tracker.BLL.DTO.Tournaments;
using Chess_Tournament_Tracker.Models.Entities;

namespace Chess_Tournament_Tracker.BLL.Services
{
    public interface ITournamentService
    {

        Tournament Insert(FormTournamentDTO tournament);
        void StartTournament(Guid id);
        bool Delete(Guid id);
        bool Update(FormTournamentDTO updateTournament, Guid id);
        void RegisterPlayerInTournament(Guid tournamentId, Guid UserId);
        void UnregisterPlayerInTournament(Guid tournamentId, Guid UserId);
        IEnumerable<TournamentDTO> GetAllByTen(Guid UserId,int offset = 0);
        FullTournamentDTO GetById(Guid id);
        public void NextRound(Guid id)
    }
}