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
    public class CreateApplicationController : Controller
    {

        
        onlineinternetposEntities db = new onlineinternetposEntities();

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult ManageApplication()
        {
            CreateApplicationModel mod = new CreateApplicationModel();
            return View(mod);
        }

        public IEnumerable<POSEntity.Menu_ListGetByParentID_Result1> MenuList(int parentid)
        {


            return db.Menu_ListGetByParentID(parentid).ToList();
        }

        [HttpPost]
        public JsonResult LoadDataForCreateApplication(string id, string parentid)
        {
            int _parentid = 0;
            if (parentid == null || parentid == "" || parentid == "null")
            {
                //Console.WriteLine("1");
                _parentid = 0;
            }
            else
            {
                _parentid = Convert.ToInt32(parentid);
            }

            
            try
            {
                IEnumerable<Menu_ListGetByParentID_Result1> lst = MenuList(_parentid);
                int recordsTotal = 0;
                recordsTotal = lst.Count();
                var data = lst;//.ToList();
                return Json(new { Result = "OK", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpPost]
        public ActionResult AddEditApplicationSave(FormCollection frm)
        {
              Menu_List objMenu;
            int _hdnmenuId = 0;

            if (ModelState.IsValid)
            {
                _hdnmenuId = Convert.ToInt32(frm["menuID"].ToString());
              
                if (_hdnmenuId > 0)
                {
                    objMenu = db.Menu_List.Where(o => o.menuID == _hdnmenuId).SingleOrDefault();
                }
                else
                {
                    objMenu = new Menu_List();
                    objMenu.menuParentID = Convert.ToInt32(frm["menuParentID"].ToString());
                }

                objMenu.menuName = frm["menuName"].ToString();
                objMenu.actionName = frm["actionName"].ToString();
                objMenu.controllerName = frm["controllerName"].ToString();


                if (_hdnmenuId > 0)
                {

                    db.Entry(objMenu).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Menu_List.Add(objMenu);
                }

                db.SaveChanges();

              


            }
            return RedirectToAction("ManageApplication");
           

        }
        
        [HttpGet]
        public ActionResult AddEditApplication(String id, String parentid)
        {

            int _id = 0;
            int _parentid = 0;
            CreateApplicationModel mod = new CreateApplicationModel();

            if(id==null)
            {
                _parentid = Convert.ToInt32(parentid);
            }
            else
            {
                _id = Convert.ToInt32(id); ;
                _parentid = Convert.ToInt32(parentid);
            }

            Menu_List objMenu;
            int _hdMenuId = Convert.ToInt32(id);
            //if (_hdMenuId > 0)
            if(!String.IsNullOrWhiteSpace(id) &&  (id!=null)&&  (id!="null"))
            {

                objMenu = db.Menu_List.Where(o => o.menuID == _hdMenuId).SingleOrDefault();
                mod.menuName = objMenu.menuName;
                mod.actionName = objMenu.actionName;
                mod.controllerName = objMenu.controllerName;
                mod.menuParentID = Convert.ToInt32(_parentid);

            }
            else
            {
                objMenu = new Menu_List();
                mod.menuName = objMenu.menuName;
                mod.actionName = objMenu.actionName;
                mod.controllerName = objMenu.controllerName;
                mod.menuParentID = Convert.ToInt32(_parentid);

            }
            return View(mod);


        }

    }
}
