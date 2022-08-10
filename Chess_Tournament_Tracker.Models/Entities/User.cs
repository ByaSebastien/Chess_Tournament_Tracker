using Chess_Tournament_Tracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid Salt { get; set; }
        public string Pseudo { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime Birthate { get; set; }
        public UserGender Gender { get; set; }
        public int ELO { get; set; } = 1200;
        public bool IsAdmin { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
        public ICollection<Game> GamesAsWhite { get; set; } = new List<Game>();
        public ICollection<Game> GamesAsBlack { get; set; } = new List<Game>();
    }
}
