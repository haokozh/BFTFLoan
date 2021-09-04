using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Attributes
{
    public class RangeOfDateOfBirthAttribute : ValidationAttribute
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            DateTime dateOfBirth = Convert.ToDateTime(value);

            if (dateOfBirth.AddYears(MinAge) > DateTime.Now) return false;

            return dateOfBirth.AddYears(MaxAge) > DateTime.Now;
        }
    }
}