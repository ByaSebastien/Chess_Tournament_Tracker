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
        public static Tournament ToDAL(this FormTournamentDTO tournament, Tournament? t = null)
        {
            t = t ?? new Tournament();

            t.Name = tournament.Name;
            t.Location = tournament.Location;
            t.MinPlayer = tournament.MinPlayer;
            t.MaxPlayer = tournament.MaxPlayer;
            t.MinELO = tournament.MinELO;
            t.MaxELO = tournament.MaxELO;
            t.Category = tournament.Category;
            t.Status = tournament.Status;
            t.IsWomenOnly = tournament.IsWomenOnly;
            t.StartDate = tournament.StartDate;
            t.EndInscription = tournament.EndInscription;

            return t;
        }

    }
}
