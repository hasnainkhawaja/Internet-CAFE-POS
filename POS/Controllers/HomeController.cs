using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSEntity;
using POS.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using POS.Helpers;

namespace POS.Controllers
{
    public class HomeController : Controller
    {

        onlineinternetposEntities db = new onlineinternetposEntities();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

           // ViewBag.Message = ClientSession.EmployeeID.ToString();
               
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Message = "Dashboard.";

            return View();
        }


        public PartialViewResult GetMenu()
        {
            var result = (from m in db.Menu_List
                          select new Models.MenuModel
                          {
                              menuID = m.menuID,
                              menuParentID = m.menuParentID,
                              menuName = m.menuName,
                              controllerName = m.controllerName,
                              actionName = m.actionName,
                          }).ToList();

            return PartialView("Menu", result); //make menu a partial view 
        }


        //public ActionResult GetMenuList12332()
        ////public ActionResult Menu()
        //{
        //    try
        //    {
        //        var result = (from m in db.Menu_List
        //                      select new Models.MenuModel
        //                      {
        //                          menuID = m.menuID,
        //                          menuParentID = m.menuParentID,
        //                          menuName = m.menuName,
        //                          controllerName = m.controllerName,
        //                          actionName = m.actionName,
        //                      }).ToList();
        //        return View("Menu", result);
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message.ToString();
        //        return Content("Error");
        //    }
        //} 
    }
}
