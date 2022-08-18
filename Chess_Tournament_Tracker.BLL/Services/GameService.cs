using Chess_Tournament_Tracker.BLL.Exceptions;
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
    public class GameService : IGameService
    {
        private readonly IGameRepository _GameRepository;
        private readonly ITournamentRepository _tournamentRepository;
        public GameService(IGameRepository gameRepository,ITournamentRepository tournamentRepository)
        {
            _GameRepository = gameRepository;
            _tournamentRepository = tournamentRepository;
        }
        public void SetResult(Guid id, GameResult result)
        {
            Game? game = _GameRepository.FindOne(id);
            Tournament? tournament = _tournamentRepository.FindOne(id);
            if (game is null) throw new KeyNotFoundException("Game doesn't exist");
            if (tournament is null) throw new KeyNotFoundException("Tournament doesn't exist");
            if (game.Round != tournament.CurrentRound) throw new TournamentRulesException("Cannot change a game in a past round");
            game.Result = result;
            _GameRepository.Update(game);
        }
    }
}
