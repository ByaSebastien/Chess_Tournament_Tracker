using Chess_Tournament_Tracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.DTO.Users
{
    public class PlayerScoreDTO
    {
        public PlayerScoreDTO(User user)
        {
            Id = user.Id;
            Pseudo = user.Pseudo;
        }

        public Guid Id { get; set; }
        public string Pseudo { get; set; }
        public int GamePlayed { get; set; }
        public int GameWon { get; set; }
        public int GameLost { get; set; }
        public int GameDrawn { get; set; }
        public float Score { get; set; }
    }
}
