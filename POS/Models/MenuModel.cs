﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS.Models
{
    public class MenuModel
    {

        public int menuID { set; get; }
        public int? menuParentID { set; get; }
        public string menuName { set; get; }
        public string controllerName { set; get; }
        public string actionName { set; get; }

    }
}