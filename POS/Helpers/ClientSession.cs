using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS.Helpers
{
    public class ClientSession
    {
        //public int Storeid { set; get; }

        //public String StoreName { set; get; }

        public static string UserName
        {
            get { return Convert.ToString(System.Web.HttpContext.Current.Session["userName"]); }
            set { System.Web.HttpContext.Current.Session["userName"] = value; }
        }

        public static string Password
        {
            get { return Convert.ToString(System.Web.HttpContext.Current.Session["Password"]); }
            set { System.Web.HttpContext.Current.Session["Password"] = value; }
        }

        public static int CompanyID
        {
            get { return Convert.ToInt32(System.Web.HttpContext.Current.Session["CompanyID"]); }
            set { System.Web.HttpContext.Current.Session["CompanyID"] = value; }
        }
        public static int EmployeeID
        {
            get { return Convert.ToInt32(System.Web.HttpContext.Current.Session["EmployeeID"]); }
            set { System.Web.HttpContext.Current.Session["EmployeeID"] = value; }
        }
    }
}