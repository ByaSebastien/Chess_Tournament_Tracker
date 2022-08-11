using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.DTO.Users
{
    public class LoginDTO
    {
        [Required]
        public string Login { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
