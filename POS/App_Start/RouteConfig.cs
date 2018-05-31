using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace POS
{

    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                
                //defaults: new { controller = "Home", action = "Dashboard", id = UrlParameter.Optional }
                //defaults: new { controller = "Users", action = "ShowGrid", id = UrlParameter.Optional }
                
                
              // defaults: new { controller = "Store", action = "ManageStore", id = UrlParameter.Optional }

               // defaults: new { controller = "Designation", action = "ManageDesignations", id = UrlParameter.Optional }

                        //defaults: new { controller = "Rate", action = "ManageRate", id = UrlParameter.Optional }
                       //  defaults: new { controller = "Shift", action = "ManageShift", id = UrlParameter.Optional }
               //defaults: new { controller = "StoreTerminal", action = "ManageStoreTerminal", id = UrlParameter.Optional }
               defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
  
            );
        }
    }
}