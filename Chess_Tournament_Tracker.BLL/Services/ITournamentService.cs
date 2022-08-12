using Chess_Tournament_Tracker.BLL.DTO.Tournaments;
using Chess_Tournament_Tracker.Models.Entities;

namespace Chess_Tournament_Tracker.BLL.Services
{
    public interface ITournamentService
    {
        Tournament Insert(FormTournamentDTO tournament);
        void AddPlayer(Guid UserId);
        bool Delete(Guid id);
        void DeletePlayer(Guid UserId);
        IEnumerable<Tournament> GetLastTenTournamentsInProgressOnDateDescending();
        Tournament GetById(Guid id);
        bool Update(FormTournamentDTO tournament, Guid id);
    }
}