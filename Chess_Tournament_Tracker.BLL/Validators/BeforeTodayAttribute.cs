using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.Validators
{
    public class BeforeTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime? date = value as DateTime?;
            return date is not null ? date < DateTime.Now : true;
        }
    }
}
