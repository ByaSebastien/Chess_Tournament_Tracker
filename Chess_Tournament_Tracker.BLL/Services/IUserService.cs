using Chess_Tournament_Tracker.BLL.DTO.Users;
using Chess_Tournament_Tracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.Services
{
    public interface IUserService
    {
        User GetById(Guid id);
        void Register(RegisterDTO user);
        string Login(LoginDTO user);
        bool Update(User user);
        bool Delete(Guid id);
    }
}
