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
    public class StoreTerminalModel
    {

        public Int32? terminalID { get; set; }


        public IEnumerable<SelectListItem> Floor { get; set; }


        [Display(Name = "Select Floor")]
        [Required]
        [OptionVerificationAttribute]
        public int SelectedFloorId { get; set; }

        public string terminalCode { get; set; }


        [Display(Name = "Enter Title")]
        [Required]
        public string title { get; set; }
       
        public Guid? connectionCode { get; set; }

        public int? floorid { get; set; }

        [Display(Name = "Enter Color")]
        [Required]
        public string color { get; set; }



        public Boolean active { get; set; }

        public Int32 createdBy { get; set; }

        public DateTime createdDate { get; set; }
        public Int32 updatedBy { get; set; }

        public DateTime updatedDate { get; set; }
          
    }
}