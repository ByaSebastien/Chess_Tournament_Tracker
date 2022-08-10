using Chess_Tournament_Tracker.BLL.DTO.Users;
using Chess_Tournament_Tracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.Mappers
{
    public static class UserMapper
    {
        public static User ToDAL(this RegisterDTO user)
        {
            return new User
            {
                Pseudo = user.Pseudo,
                Mail = user.Mail,
                Birthate = user.Birthate,
                Gender = user.Gender,
            };
        }
    }
}
