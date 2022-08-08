using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.Models.Enums
{
    [Flags]
    public enum TournamentCategory
    {
        Junior = 1,
        Senior = 2,
        Veteran = 4,
    }
}
