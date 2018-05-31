using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POS.Helpers
{
    public class TimeFormatAttribute : ValidationAttribute
    { 

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        { 
            try{
               
                var date = DateTime.Parse(value.ToString());
                return null;
            }
            catch(Exception ex)
            {
                 return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
             
            
        }
    }
}