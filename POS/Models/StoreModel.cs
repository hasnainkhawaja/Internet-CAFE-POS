using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POS.Models
{
    public class StoreModel
    {

        public Int32 storeID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "storeCode")]
        public string storeCode { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "storeName")]
        public string storeName { get; set; }

        public Boolean active { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "servicestartDate")]
        public String servicestartDate { get; set; }
        //public DateTime servicestartDate { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "phoneNo")]
        public String phoneNo { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "address")]
        public String address { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "terminationDate")]
        public String terminationDate { get; set; }
        public Int32 companyId { get; set; }
        public Int32 createdBy { get; set; }

        public DateTime createdDate { get; set; }
        public Int32 updatedBy { get; set; }

        public DateTime updatedDate { get; set; }
          
    }
}