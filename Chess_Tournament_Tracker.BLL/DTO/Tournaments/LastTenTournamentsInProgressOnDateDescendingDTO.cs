using Chess_Tournament_Tracker.Models.Entities;
using Chess_Tournament_Tracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.DTO.Tournaments
{
    
    public class LastTenTournamentsInProgressOnDateDescendingDTO
    {
        public LastTenTournamentsInProgressOnDateDescendingDTO(Tournament tournament)
        {
            Id = tournament.Id;
            CountPlayer = tournament.Users.Count;
            Name = tournament.Name;
            Location = tournament.Location;
            MinPlayer = tournament.MinPlayer;
            MaxPlayer = tournament.MaxPlayer;
            MinELO = tournament.MinELO;
            MaxELO = tournament.MaxELO;
            Category = tournament.Category;
            Status = tournament.Status;
            CurrentRound = tournament.CurrentRound;

        }
        public Guid Id { get; set; }
        public int CountPlayer { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Location { get; set; } = String.Empty;
        public int MinPlayer { get; set; }
        public int MaxPlayer { get; set; }
        public int MinELO { get; set; }
        public int MaxELO { get; set; }
        public TournamentCategory Category { get; set; }
        public TournamentStatus Status { get; set; } = TournamentStatus.WaitingPlayer;
        public int CurrentRound { get; set; } = 0;

    }
}
