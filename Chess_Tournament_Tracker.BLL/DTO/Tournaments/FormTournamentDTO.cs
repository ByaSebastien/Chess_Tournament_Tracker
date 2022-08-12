using Chess_Tournament_Tracker.BLL.Validators;
using Chess_Tournament_Tracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.DTO.Tournaments
{
    public class FormTournamentDTO
    {
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Location { get; set; } = String.Empty;
        [Range(2,32)]
        public int MinPlayer { get; set; }

        [IsGreaterThan(nameof(MinPlayer))]
        [Range(2,32)]
        public int MaxPlayer { get; set; }
        [Range(0,3000)]
        public int MinELO { get; set; }

        [IsGreaterThan(nameof(MinELO))]
        [Range(0,3000)]
        public int MaxELO { get; set; }
        public TournamentCategory Category { get; set; }
        public TournamentStatus Status { get; set; } = TournamentStatus.WaitingPlayer;
        public bool IsWomenOnly { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndInscription { get; set; }
    }
}
