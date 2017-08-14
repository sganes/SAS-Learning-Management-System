using System;
using System.ComponentModel.DataAnnotations;

namespace SAS_LMS.Models
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            string dateString = Convert.ToString(value);
            var isValid = DateTime.TryParse(dateString, out dateTime);
            return (isValid && dateTime > DateTime.Now);
        }

    }
}



