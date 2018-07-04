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
    public class CreateApplicationModel
    {

        public int menuID { set; get; }
        public int menuParentID { set; get; }
        
        [Required(ErrorMessage = "*")]
        [Display(Name = "MenuName")]
        public String menuName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "ControllerName")]
        public String controllerName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "ActionName")]
        public String actionName { get; set; }

    }
}