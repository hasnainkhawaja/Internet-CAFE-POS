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
    public class EmployeeRoleModel
    {

        public int employeeroleid { set; get; }
        public int employeeid { set; get; }
         //public int designationId { set; get; }

        //public int storeid { set; get; }

        public Boolean isdefault { set; get; }
        
        public int createdBy { set; get; }
        public DateTime createdDate { set; get; }

        public IEnumerable<SelectListItem> Stores { get; set; }

        [Display(Name = "Store")]
        [Required]
        [OptionVerificationAttribute]
        public int SelectedStoreId { get; set; }



        public IEnumerable<SelectListItem> Designations { get; set; }

        [Display(Name = "Designations")]
        [Required]
        [OptionVerificationAttribute]
        public int SelectedDesignationsId { get; set; }

        public String UserName { get; set; }

    }
}