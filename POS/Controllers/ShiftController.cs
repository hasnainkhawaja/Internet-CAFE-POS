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
    public class ShiftController : Controller
    {
        //
        // GET: /Rate/
        onlineinternetposEntities db = new onlineinternetposEntities();
        public ActionResult Index()
        {
            ShiftModel mod = new ShiftModel();
            return View(mod);
        }


        public ActionResult ManageShift()
        {
            ShiftModel mod = new ShiftModel();
            return View(mod);

        }

        

        public JsonResult LoadDataForShiftDataTable()
        {
            try
            {

                IEnumerable<Shfit_SelectByCompanyID_Result> ShfitLst = db.Shfit_SelectByCompanyID(ClientSession.CompanyID).ToList();//ShiftList(); 

                int recordsTotal = 0;
              
                recordsTotal = ShfitLst.Count();
                var data = ShfitLst.ToList();
                return Json(new { Result = "OK", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpGet]
        public string GetShiftDataByID(int ShiftID)
        {
            string data = JsonConvert.SerializeObject(db.Shfit_SelectByShiftID(ShiftID));
            return "" + data;
        } 

        public ActionResult DelteShift(int id)
        {
            Shift objshift = db.Shifts.Where(o => o.shfitID == id).SingleOrDefault();
            objshift.active = false;
            db.SaveChanges();
           // return true;
            return RedirectToAction("ManageShift");
        }
         

        [HttpGet]
        public ActionResult AddEditShift(Int64 id=0) 
        {

            ShiftModel mod = new ShiftModel();

            var objshift = db.Shifts.Where(o => o.shfitID == id).SingleOrDefault();
           
            #region loadstores
            List<Store> stores = (from data in db.Stores
                                  where data.companyId == ClientSession.CompanyID
                                  select data).ToList();

            Store objStores = new Store();
            objStores.storeName = "Select";
            objStores.storeID = 0;
            stores.Insert(0, objStores);
            mod.Stores = stores.Select(a => new SelectListItem
            {
                Text = a.storeName,
                Value = a.storeID.ToString()
            });
            #endregion

            if (objshift !=null)
            {
                mod.startTime = Convert.ToDateTime(objshift.startTime).ToShortTimeString();
                mod.endTime = Convert.ToDateTime(objshift.endTime).ToShortTimeString();
                mod.SelectedStoreId = Convert.ToInt32(objshift.storeID);
                mod.editShiftId = Convert.ToInt32(id); 
                mod.shfitID = id; 
                mod.shiftCode = objshift.shiftCode;
                mod.active = Convert.ToBoolean(objshift.active);

            }
            return View("AddEditShift", mod); 

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditShift(ShiftModel mod)
        {


            if (ModelState.IsValid)
            {
                if(mod.editShiftId!=null && mod.editShiftId>0)
                { 

                    Shift objshift = db.Shifts.Where(o => o.shfitID == mod.editShiftId.Value).SingleOrDefault();


                    objshift.shiftCode = mod.shiftCode.ToString();
                    objshift.startTime = Convert.ToDateTime(mod.startTime.ToString());
                    objshift.endTime = Convert.ToDateTime(mod.endTime.ToString());


                    objshift.storeID = Convert.ToInt32(mod.SelectedStoreId);

                    var _isActive = mod.active.ToString().ToLower();
                    if (_isActive == "true,false" || _isActive == "true")
                    {
                        objshift.active = true;

                    }
                    else
                    {

                        objshift.active = false;

                    }

                    objshift.updatedBy = 1;
                    objshift.updatedDate = DateTime.Now;
                    db.Entry(objshift).State = EntityState.Modified;
                    db.SaveChanges(); 


                    return RedirectToAction("ManageShift");

                }
                else
                {
                    String _isActive = "";
                    Shift objshift = new Shift();
                    objshift.shiftCode = mod.shiftCode;
                    objshift.startTime = Convert.ToDateTime(mod.startTime);
                    objshift.endTime = Convert.ToDateTime(mod.endTime);
                    objshift.storeID = Convert.ToInt32(mod.SelectedStoreId);
                    _isActive = mod.active.ToString();
                    if (_isActive.ToLower() == "true,false" || _isActive == "true")
                    {
                        objshift.active = true;
                    }
                    else
                    {
                        objshift.active = false;
                    }

                    objshift.createdBy = 1233;
                    objshift.createdDate = DateTime.Now;
                    db.Shifts.Add(objshift);
                    db.SaveChanges();
                   
                }

                return RedirectToAction("ManageShift");
            }
            else
            {


                #region loadstores
                List<Store> stores = (from data in db.Stores
                                      where data.companyId == ClientSession.CompanyID
                                      select data).ToList();

                Store objStores = new Store();
                objStores.storeName = "Select";
                objStores.storeID = 0;
                stores.Insert(0, objStores);
                mod.Stores = stores.Select(a => new SelectListItem
                {
                    Text = a.storeName,
                    Value = a.storeID.ToString()
                });
                #endregion
            }

            //validation error
            return View("AddEditShift", mod); 
        }
      
       
    }
}
