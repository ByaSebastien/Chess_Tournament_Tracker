using Chess_Tournament_Tracker.BLL.Validators;
using Chess_Tournament_Tracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.DTO.Tournament
{
    public class InsertTournamentDTO
    {
        [Required]
        public string Name { get; set; } = String.Empty;
        public string Location { get; set; } = String.Empty;
        public int MinPlayer { get; set; }

        [IsGreaterThan(nameof(MinPlayer))]
        public int MaxPlayer { get; set; }
        public int MinELO { get; set; }

        [IsGreaterThan(nameof(MinELO))]
        public int MaxELO { get; set; }
        public TournamentCategory Category { get; set; }
        public TournamentStatus Status { get; set; } = TournamentStatus.WaitingPlayer;
        public bool IsWomenOnly { get; set; }
        public DateTime EndInscription { get; set; }
    }
}
