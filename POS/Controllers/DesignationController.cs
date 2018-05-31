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
    public class DesignationController : Controller
    {

        
        onlineinternetposEntities db = new onlineinternetposEntities();

        //
        // GET: /Designation/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult ManageDesignations()
        {
            DesignationModel mod = new DesignationModel();
            return View(mod);
        }

        public IEnumerable<POSEntity.Designations_SelectAll_Result> DesignationList()
        {

            return db.Designations_SelectAll(1, "1,0").ToList();
        }


        public JsonResult LoadDataForDestignationsDataTable()
        {
            try
            {

                IEnumerable<Designations_SelectAll_Result> des;
                des = DesignationList();

                int recordsTotal = 0;
                //var customerData = (from tempcustomer in db.Designations
                //                    select tempcustomer);
                //customerData.Where(a => a.companyId == 1);

                recordsTotal = des.Count();
                var data = des.ToList();               
                return Json(new { Result = "OK", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        
        public ActionResult DeleteDesignations(int id)
        {
            Designation objdes = db.Designations.Where(o => o.designationId == id).SingleOrDefault();
            db.Designations.Remove(objdes);
            db.SaveChanges();
            
            //return true;
            return RedirectToAction("ManageDesignations", "Designation");
        }

        //[HttpPost]
        //public ActionResult DeleteDesignation(int DesignationID, String status)
        //{
        //    Designation objdes = db.Designations.Where(o => o.designationId == DesignationID).SingleOrDefault();
        //    Boolean _status = false; 
        //       if(status=="true")
        //       {_status =true; }

        //       objdes.active = _status;
        //    db.SaveChanges();
        //    //return true;
        //    return RedirectToAction("ManagerUsers","Users");
        //}
        

        [HttpPost]
        public ActionResult AddEditDesignationSave(FormCollection frm)
        {

            int _hdndesId = 0;
            int _companyId = ClientSession.CompanyID;
            String _isActive = "";
            Designation objdes;


            if (ModelState.IsValid)
            {
                _hdndesId = Convert.ToInt32(frm["designationId"].ToString());

                if (_hdndesId > 0)
                {

                    objdes = db.Designations.Where(o => o.designationId == _hdndesId).SingleOrDefault();


                }
                else
                {
                    objdes = new Designation();
                }



                objdes.title = frm["title"].ToString();
                objdes.companyId = _companyId;
                objdes.active = true;

                if (_hdndesId > 0)
                {
                    objdes.modifiedBy = 1;
                    objdes.modifiedDate = DateTime.Now;
                    db.Entry(objdes).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    objdes.createdBy = 1233;
                    objdes.createdDate = DateTime.Now;
                    db.Designations.Add(objdes);
                }

                db.SaveChanges();




            }

            //Users/LoadDataForDataTable
            return RedirectToAction("ManageDesignations");

        }

        public ActionResult AddEditDesignations(String id)
        {
            DesignationModel mod = new DesignationModel();
            Designation objdes;
            int _hdndesId = Convert.ToInt32(id);
            if (_hdndesId > 0)
            {

                objdes = db.Designations.Where(o => o.designationId == _hdndesId).SingleOrDefault();


            }
            else
            {
                objdes = new Designation();
            }



            mod.designationId = _hdndesId;
            mod.title = objdes.title;
            
            return View(mod);


        }

    }
}
