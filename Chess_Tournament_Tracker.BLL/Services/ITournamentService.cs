using Chess_Tournament_Tracker.BLL.DTO.Tournament;
using Chess_Tournament_Tracker.Models.Entities;

namespace Chess_Tournament_Tracker.BLL.Services
{
    public interface ITournamentService
    {
        Tournament Insert(FormTournamentDTO tournament);
        void AddPlayer(Guid UserId);
        bool Delete(Guid id);
        void DeletePlayer(Guid UserId);
        IEnumerable<Tournament> GetAll();
        Tournament GetById(Guid id);
        bool Update(FormTournamentDTO tournament, Guid id);
    }
}