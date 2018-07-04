using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using POSEntity;
using System.Web.Mvc;
using POS.Helpers;

namespace POS.Models
{
    public class TaxCategoryModel
    {
  
        public Int32 taxid { get; set; }

        
        [Display(Name = "Title")]
        [Required]
        public String title { get; set; }


        [Display(Name = "Rate")]
        [Required]
        public Decimal rate { get; set; }

        [Display(Name = "Status")]
        public Boolean active { get; set; }
         

        public Int32 companyID { get; set; }
         

    }





}