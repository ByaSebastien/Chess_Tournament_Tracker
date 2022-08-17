using Chess_Tournament_Tracker.BLL.DTO.Users;
using Chess_Tournament_Tracker.Models.Entities;
using Chess_Tournament_Tracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.DTO.Games
{
    public class GameDTO
    {
        public GameDTO(Game game)
        {
            Id = game.Id;
            Black = new PlayerDTO(game.Black);
            White = new PlayerDTO(game.White);
            Result = game.Result;
            Round = game.Round;
        }

        public Guid Id { get; set; }
        public PlayerDTO Black { get; set; }
        public PlayerDTO White { get; set; }
        public GameResult Result { get; set; }
        public int Round { get; set; }
    }
}
