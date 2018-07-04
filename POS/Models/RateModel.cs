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
    public class RateModel
    {

        public IEnumerable<SelectListItem> Stores { get; set; }
       
        [Display(Name = "Store")]
        [Required]
        [OptionVerificationAttribute]
        public int SelectedStoreId { get; set; }


        public IEnumerable<SelectListItem> RateTypes { get; set; }
        [Display(Name = "Type")]
        [Required]
        [OptionVerificationAttribute]
        public int SelectedRateId { get; set; }

      
//public int RateType { get; set; }
 

        public Int32 rateID { get; set; }

        [Display(Name = "Rate Code")]
        [Required]
        public String rateCode { get; set; }




        [Display(Name = "Title")]
        [Required]
        public String title { get; set; }



        
        [Display(Name = "Amount")]
        [Required]
        public Decimal amount { get; set; }

        public Boolean active { get; set; }

        public Int32 storeID { get; set; }

        public Int32 companyID { get; set; }
        
        public Boolean isPrepay { get; set; }


        
        [Display(Name = "Start Time")]
        [Required]
        [TimeFormatAttribute]
        [GenericCompare(CompareToPropertyName = "endTime", OperatorName = GenericCompareOperator.LessThan)]
        public String startTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        [TimeFormatAttribute]
        [GenericCompare(CompareToPropertyName = "startTime", OperatorName = GenericCompareOperator.GreaterThan)]
        public String endTime { get; set; }

        [Required]
        [Display(Name = "Color")]
        public String color { get; set; }

        [Display(Name = "Buffer Time")]
        public Int32 bufferTime { get; set; }

        [Display(Name = "Alert Interval")]
        public Int32 alertInterval { get; set; }


        public Int32 createdBy { get; set; }

        public DateTime Createddate { get; set; }

        public Int32 updatedBy { get; set; }


        public DateTime updatedDate { get; set; }

    }





}