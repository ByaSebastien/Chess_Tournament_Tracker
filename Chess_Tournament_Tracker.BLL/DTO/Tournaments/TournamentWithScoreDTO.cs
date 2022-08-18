using Chess_Tournament_Tracker.BLL.DTO.Users;
using Chess_Tournament_Tracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.DTO.Tournaments
{
    public class TournamentWithScoreDTO : TournamentDTO
    {
        public TournamentWithScoreDTO(Tournament tournament) : base(tournament)
        {
        }
        public ICollection<PlayerScoreDTO>? Players { get; set; }
    }
}
