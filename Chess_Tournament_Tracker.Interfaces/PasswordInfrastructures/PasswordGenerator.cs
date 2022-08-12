using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.IL.PasswordInfrastructures
{
    public static class PasswordGenerator
    {
        public static string Create(int length)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}
