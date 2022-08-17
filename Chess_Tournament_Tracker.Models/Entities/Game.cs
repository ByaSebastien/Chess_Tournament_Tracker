using Chess_Tournament_Tracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.Models.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid BlackId { get; set; }
        public Guid WhiteId { get; set; }
        public Guid TournamentId { get; set; }
        public GameResult Result { get; set; }
        public int Round { get; set; }
        public User Black { get; set; } = new User();
        public User White { get; set; } = new User();
        public Tournament CurrentTournament { get; set; } = new Tournament();
    }
}
