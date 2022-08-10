using Chess_Tournament_Tracker.BLL.DTO.Tournament;
using Chess_Tournament_Tracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.Mappers
{
    public static class TournamentMapper
    {
        public static Tournament ToDAL(this InsertTournamentDTO tournament)
        {
            return new Tournament
            {
                Name = tournament.Name,
                Location = tournament.Location,
                MinPlayer = tournament.MinPlayer,
                MaxPlayer = tournament.MaxPlayer,
                MinELO = tournament.MinELO,
                MaxELO = tournament.MaxELO,
                Category = tournament.Category,
                Status = tournament.Status,
                IsWomenOnly = tournament.IsWomenOnly,
                EndInscription = tournament.EndInscription,
            };
        }

    }
}
