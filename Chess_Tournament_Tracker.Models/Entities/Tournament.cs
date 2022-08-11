using Chess_Tournament_Tracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.Models.Entities
{
    public class Tournament
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Location { get; set; } = String.Empty;
        public int MinPlayer { get; set; }
        public int MaxPlayer { get; set; }
        public int MinELO { get; set; }
        public int MaxELO { get; set; }
        public TournamentCategory Category { get; set; }
        public TournamentStatus Status { get; set; } = TournamentStatus.WaitingPlayer;
        public int CurrentRound { get; set; } = 0;
        public bool IsWomenOnly { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndInscription { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
