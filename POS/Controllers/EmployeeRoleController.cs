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
    public class EmployeeRoleController : Controller
    {

        
        onlineinternetposEntities db = new onlineinternetposEntities();

        //
        // GET: /Designation/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult ManageEmployeeRole()
        {
            EmployeeRoleModel mod = new EmployeeRoleModel();
            return View(mod);
        }

        public IEnumerable<POSEntity.EmployeeRole_GetByEmployeeID_Result> EmployeeRoleList(int employeeid,int companyid )
        {

            return db.EmployeeRole_GetByEmployeeID(employeeid, companyid).ToList();
        }

        [HttpPost]
        public JsonResult LoadDataForEmployeeRoleDataTable(string empid)
        {
            int employeeid = Convert.ToInt32(empid);
            try
            {

                IEnumerable<EmployeeRole_GetByEmployeeID_Result> des;
                des = EmployeeRoleList(employeeid,ClientSession.CompanyID);
                int recordsTotal = 0;
                recordsTotal = des.Count();
                var data = des.ToList();
                return Json(new { Result = "OK", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data, JsonRequestBehavior.AllowGet });
                //return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }

        }


        public ActionResult DeleteEmployeeRole(int id, String empid)
        {
            EmployeeRole objdes = db.EmployeeRoles.Where(o => o.employeeroleid == id).SingleOrDefault();
            db.EmployeeRoles.Remove(objdes);
            db.SaveChanges();
            
            //return true;
            //return RedirectToAction("ManageDesignations", "Designation");

            return RedirectToAction("/ManageEmployeeRole/" + empid);
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
        public ActionResult AddEditEmployeeRoleSave(FormCollection frm)
        {

            int _hdnEmpRoleId = 0;
            int _hdnEmpId = 0;
            int _companyId = ClientSession.CompanyID;
            String _isdefault = "";
            EmployeeRole objdes;


            if (ModelState.IsValid)
            {
                _hdnEmpRoleId = Convert.ToInt32(frm["employeeroleid"].ToString());
                _hdnEmpId = Convert.ToInt32(frm["employeeid"].ToString());
                if (_hdnEmpRoleId > 0)
                {

                    objdes = db.EmployeeRoles.Where(o => o.employeeroleid == _hdnEmpRoleId).SingleOrDefault();


                }
                else
                {
                    objdes = new EmployeeRole();
                }


                objdes.employeeid = _hdnEmpId;
                objdes.designationId = Convert.ToInt32(frm["SelectedDesignationsId"].ToString());
                objdes.storeid = Convert.ToInt32(frm["SelectedStoreId"].ToString());


                _isdefault = frm["isdefault"].ToString();
                if (_isdefault == "true,false" || _isdefault == "true")
                {
                    objdes.isdefault = true;
                    
                }
                else
                {

                    objdes.isdefault = false;
                   
                }

                if (_hdnEmpRoleId > 0)
                {
                 
                    db.Entry(objdes).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    objdes.createdBy = 1233;
                    objdes.createdDate = DateTime.Now;
                    db.EmployeeRoles.Add(objdes);
                }

                db.SaveChanges();




            }

            //Users/LoadDataForDataTable
            return RedirectToAction("/ManageEmployeeRole/" + _hdnEmpId);

        }
        
        [HttpGet]
        public ActionResult AddEditEmployeeRole(String id, String empid)
        {
     
            EmployeeRoleModel mod = new EmployeeRoleModel();


            #region loadstoresAndDesignation

            List<Store> lstStores = (from data in db.Stores
                                    select data).ToList();

            Store objcountry = new Store();
            objcountry.storeName = "Select";
            objcountry.storeID = 0;
            lstStores.Insert(0, objcountry);
            mod.Stores = lstStores.Select(a => new SelectListItem
            {
                Text = a.storeName,
                Value = a.storeID.ToString()
            });



            List<Designation> lstDesig = (from data in db.Designations
                                     select data).ToList();

            Designation objdes = new Designation();
            objdes.title = "Select";
            objdes.designationId = 0;
            lstDesig.Insert(0, objdes);
            mod.Designations = lstDesig.Select(a => new SelectListItem
            {
                Text = a.title,
                Value = a.designationId.ToString()
            });

            #endregion


            List<EmployeeInfomation> lstEmp = (from data in db.EmployeeInfomations
                                     select data).ToList();

            mod.UserName = lstEmp[0].userName;

            EmployeeRole objRole;
            int _hdRoleId = Convert.ToInt32(id);
            if (_hdRoleId > 0)
            {

                objRole = db.EmployeeRoles.Where(o => o.employeeroleid == _hdRoleId).SingleOrDefault();
                
                mod.SelectedStoreId = Convert.ToInt32(objRole.storeid);
                mod.SelectedDesignationsId = objRole.designationId;
                

            }
            else
            {
              objRole = new EmployeeRole();

               objRole.storeid=mod.SelectedStoreId;
               objRole.designationId = mod.SelectedDesignationsId;

            }
            mod.employeeroleid = Convert.ToInt32(_hdRoleId); 
            mod.employeeid = Convert.ToInt32(empid);
            mod.isdefault = objRole.isdefault;


          //  mod.designationId = _hdndesId;
          //  mod.title = objdes.title;
            
            return View(mod);


        }

    }
}
