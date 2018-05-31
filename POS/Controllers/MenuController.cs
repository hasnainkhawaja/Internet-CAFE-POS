using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult BCS_Action()
        {
            return RedirectToAction("ManageUsers", "Users");
        }      
  
        public ActionResult Computer_Action()
        {
            return RedirectToAction("ManageUsers", "Users");
        }      
  
          
        public ActionResult MCS_Action()
        {
            return RedirectToAction("ManageUsers", "Users");
        } 
          
        public ActionResult Maths_Action()
        {
            return RedirectToAction("ManageUsers", "Users");
        } 
          
        public ActionResult Marketing_Action()
        {
            return RedirectToAction("ManageUsers", "Users");
        }

        public ActionResult Finiance_Action()
        {
            return RedirectToAction("ManageUsers", "Users");
        } 

        




    }
}
