using Chess_Tournament_Tracker.Models.Enums;

namespace Chess_Tournament_Tracker.BLL.Services
{
    public interface IGameService
    {
        void SetResult(Guid id, GameResult result);
    }
}