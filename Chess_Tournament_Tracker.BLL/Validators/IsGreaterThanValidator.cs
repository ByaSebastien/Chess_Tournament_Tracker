using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.BLL.Validators
{
    public class IsGreaterThanAttribute : ValidationAttribute
    {
        public string PropName { get; set; }

        public IsGreaterThanAttribute(string propName)
        {
            PropName = propName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Type t = validationContext.ObjectInstance.GetType();
            PropertyInfo? prop = t.GetProperty(PropName);
            IComparable? max = value as IComparable;
            IComparable? min = prop?.GetValue(validationContext.ObjectInstance) as IComparable;
            return min?.CompareTo(max) <= 0 ? null : new ValidationResult("Attention ");
        }
    }
}
