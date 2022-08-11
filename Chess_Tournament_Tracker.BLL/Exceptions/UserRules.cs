using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.Exceptions
{
    public class UserRules : Exception
    {
        public UserRules(string message) : base(message) { }
    }
}
