using Chess_Tournament_Tracker.BLL.Validators;
using Chess_Tournament_Tracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.DTO.Users
{
    public class RegisterDTO
    {
        public string Pseudo { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [BeforeToday]
        [Required]
        public DateTime Birthate { get; set; }
        public UserGender Gender { get; set; }
        public int? ELO { get; set; } = 1200;
    }
}
