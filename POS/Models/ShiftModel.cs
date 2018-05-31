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
    public class ShiftModel
    {


        public IEnumerable<SelectListItem> Stores { get; set; }


        [Display(Name = "Store")]
        [Required]
        [OptionVerificationAttribute]
        public int SelectedStoreId { get; set; }
        public int shfitID { get; set; }
      
        public int? editShiftId { get; set; }

        [Required]
        [Display(Name = "Shift Code")]
        public String shiftCode { get; set; }


        [Required]
        [Display(Name = "start Time")]
        public String startTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public String endTime { get; set; }


        public Boolean active { get; set; }

        public Int32 createdBy { get; set; }

        public DateTime Createddate { get; set; }

        public Int32 updatedBy { get; set; }


        public DateTime updatedDate { get; set; }


    }
}