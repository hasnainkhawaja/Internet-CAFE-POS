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
    public class AssignApplicationModel
    {


        public int appCompanyMapID { set; get; }
        public int companyID { set; get; }
        
        public int menuID { set; get; }
        public IEnumerable<SelectListItem> ActionsList { get; set; }

        public string hiddevalueforRepopulate { get; set; }
        //public string SelectType { get; set; }
     
}
}

public enum AppSelectedTypes
{
    Designation,
    Employee
}

public class TreeViewNode
{
    public string id { get; set; }
    public string parent { get; set; }
    public string text { get; set; }
}