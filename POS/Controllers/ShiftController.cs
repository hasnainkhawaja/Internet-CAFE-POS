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

        //public IEnumerable<POSEntity.Store_SelectByCompanyID_Result> StoreList()
        //{
        //    return db.Store_SelectByCompanyID(1).ToList();
        //}


        //public void PopulateStoresDropDownList(object selectedDepartment = null)
        //{
        //    IEnumerable<Store_SelectByCompanyID_Result> streLst = StoreList();

            
        //    ViewBag.DepartmentID = new SelectList(streLst, "storeID", "storeName", selectedDepartment);
        //}

        public JsonResult LoadDataForShiftDataTable()
        {
            try
            {

                IEnumerable<Shfit_SelectByCompanyID_Result> ShfitLst = db.Shfit_SelectByCompanyID(ClientSession.CompanyID).ToList();//ShiftList();


                int recordsTotal = 0;
                //var storeData = (from tempStore in db.Stores
                //                 select tempStore);
                //storeData.Where(a => a.companyId == 1);

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

        public ActionResult AddShift()
        {
            int _companyid = ClientSession.CompanyID;
            Random rand = new Random();

            int _newValue = rand.Next();

            ShiftModel mod = new ShiftModel();

            #region loadstores
            List<Store> stores = (from data in db.Stores
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
            

            mod.shiftCode = _companyid + "-" + _newValue;

            return View(mod);
        }


        public ActionResult EditShift(String id)
       // public ActionResult AddEditShift(String id)
        {

            ShiftModel mod = new ShiftModel();

            List<Store> accounts = (from data in db.Stores
                                    select data).ToList();

            Store objcountry = new Store();
            objcountry.storeName = "Select";
            objcountry.storeID = 0;
            accounts.Insert(0, objcountry);
            mod.Stores = accounts.Select(a => new SelectListItem
           {
               Text = a.storeName,
               Value = a.storeID.ToString()
           });



            Shift objshift;
            int _hdnshiftId = Convert.ToInt32(id);
            if (_hdnshiftId > 0)
            {

                objshift = db.Shifts.Where(o => o.shfitID == _hdnshiftId).SingleOrDefault();
                mod.startTime = Convert.ToDateTime(objshift.startTime).ToShortTimeString();
                mod.endTime = Convert.ToDateTime(objshift.endTime).ToShortTimeString();
                mod.SelectedStoreId = Convert.ToInt32(objshift.storeID);
                mod.editShiftId = Convert.ToInt32(id);
           
            mod.shfitID = _hdnshiftId;
            mod.shiftCode = objshift.shiftCode;
            mod.active = Convert.ToBoolean(objshift.active);

            }
            return View(mod);


        }


      
          [HttpPost]
        public ActionResult AddShift(ShiftModel mod)
        {


            //ShiftModel mod = new ShiftModel();

            //List<Store> accounts = (from data in db.Stores
            //                        select data).ToList();

            //Store objcountry = new Store();
            //objcountry.storeName = "Select";
            //objcountry.storeID = 0;
            //accounts.Insert(0, objcountry);
            //mod.Stores = accounts.Select(a => new SelectListItem
            //{
            //    Text = a.storeName,
            //    Value = a.storeID.ToString()
            //});



            if(ModelState.IsValid)
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

            return RedirectToAction("ManageShift");

            }
              else
            {

                #region loadstores
                List<Store> stores = (from data in db.Stores
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
            return View(mod);

        }


          [HttpPost]
          public ActionResult EditShift(ShiftModel mod)
          {

              int _hdnshiftId = 0;
              String _isActive = "";
            
              if(mod.Stores== null)
              {

                  List<Store> accounts = (from data in db.Stores
                                          select data).ToList();

                  Store objcountry = new Store();
                  objcountry.storeName = "Select";
                  objcountry.storeID = 0;
                  accounts.Insert(0, objcountry);
                  mod.Stores = accounts.Select(a => new SelectListItem
                  {
                      Text = a.storeName,
                      Value = a.storeID.ToString()
                  });

              }


              if (ModelState.IsValid)
              {
                  _hdnshiftId = Convert.ToInt32(mod.shfitID);

               

                      Shift objshift = db.Shifts.Where(o => o.shfitID == _hdnshiftId).SingleOrDefault();




                  objshift.shiftCode = mod.shiftCode.ToString();
                  objshift.startTime = Convert.ToDateTime(mod.startTime.ToString());
                  objshift.endTime = Convert.ToDateTime(mod.endTime.ToString());


                  objshift.storeID = Convert.ToInt32(mod.SelectedStoreId);

                  _isActive = mod.active.ToString().ToLower();
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
              //else
              //{
                  
              

              //}
             
              //Users/LoadDataForDataTable
              //return RedirectToAction("ManageShift");

              
              return View(mod);
          }

        [HttpPost]
        public ActionResult AddEditShiftSave(FormCollection frm, ShiftModel mod)
        {

            int _hdnshiftId = 0;
            String _isActive = "";
            Shift objshift;
           


            if (ModelState.IsValid)
            {
                _hdnshiftId = Convert.ToInt32(frm["hdnShfitid"].ToString());

                if (_hdnshiftId > 0)
                {

                    objshift = db.Shifts.Where(o => o.shfitID == _hdnshiftId).SingleOrDefault();


                }
                else
                {
                    objshift = new Shift();
                }




                objshift.shiftCode = frm["shiftCode"].ToString();
                objshift.startTime = Convert.ToDateTime(frm["startTime"].ToString());
                objshift.endTime = Convert.ToDateTime(frm["endTime"].ToString());


                objshift.storeID = Convert.ToInt32(frm["SelectedStoreId"]); 

                _isActive = frm["active"].ToString();
                if (_isActive == "true,false" || _isActive == "true")
                {
                    objshift.active = true;
                    
                }
                else
                {

                    objshift.active = false;

                }


                if (_hdnshiftId > 0)
                {
                    objshift.updatedBy = 1;
                    objshift.updatedDate = DateTime.Now;
                    db.Entry(objshift).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    objshift.createdBy = 1233;
                    objshift.createdDate = DateTime.Now;
                    db.Shifts.Add(objshift);
                }

                db.SaveChanges();




            }

            //Users/LoadDataForDataTable
            //return RedirectToAction("ManageShift");

            return View(mod);
        }
    }
}
