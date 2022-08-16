using Chess_Tournament_Tracker.BLL.DTO.Users;
using Chess_Tournament_Tracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.DTO.Tournaments
{
    public class DetailTournamentDTO : TournamentDTO
    {
        public DetailTournamentDTO(Tournament t) : base(t) 
        {
            Games = t.Games.Where(g => g.Round == t.CurrentRound).ToList();
            Users = t.Users.Select(u => new PlayerDTO(u)).ToList();
        }
        public ICollection<PlayerDTO> Users { get; set; } = new List<PlayerDTO>();
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
