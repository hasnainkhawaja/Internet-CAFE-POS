using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POS.Models
{
    public class DesignationModel
    {

        public int designationId { set; get; }
        [Required]
        public String title { set; get; }

        public int companyId { set; get; }

        public Boolean active { set; get; }
        
        public int createdBy { set; get; }
        public DateTime createdDate	{ set; get; }

        public int modifiedBy { set; get; }
        public DateTime modifiedDate { set; get; }


    }
}