using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.Exceptions
{
    public class TournamentRulesException : Exception
    {
        public TournamentRulesException(string message) : base(message)
        {

        }

    }
}
