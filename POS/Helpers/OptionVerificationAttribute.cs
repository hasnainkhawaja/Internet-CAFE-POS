using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POS.Helpers
{
    public class OptionVerificationAttribute : ValidationAttribute
    { 

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        { 
            if (value.ToString()=="0")
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}