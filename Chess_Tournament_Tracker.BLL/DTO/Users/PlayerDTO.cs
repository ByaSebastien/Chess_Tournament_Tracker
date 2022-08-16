using Chess_Tournament_Tracker.BLL.Services;
using Chess_Tournament_Tracker.Models.Entities;
using Chess_Tournament_Tracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.DTO.Users
{
    public class PlayerDTO
    {
        public PlayerDTO(User u)
        {
            Id = u.Id;
            Pseudo = u.Pseudo;
            Mail = u.Mail;
            Gender = u.Gender;
            ELO = u.ELO;
            int age = CalculAge(DateTime.Now, u.Birthate);
            if (age < 18)
                Category = TournamentCategory.Junior;
            if (age >= 18 && age < 60)
                Category = TournamentCategory.Junior;
            if (age >= 60)
                Category = TournamentCategory.Junior;
        }


        public Guid Id { get; set; }
        public string Pseudo { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public TournamentCategory Category { get; set; }
        public UserGender Gender { get; set; }
        public int ELO { get; set; } = 1200;
        private static int CalculAge(DateTime endInscription, DateTime birthDate)
        {
            int age = endInscription.Year - birthDate.Year;
            if (birthDate > endInscription.AddYears(-age)) age--;
            return age;
        }
    }
}
