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
    public class StoreController : Controller
    {
       
        onlineinternetposEntities db = new onlineinternetposEntities();

        //
        // GET: /Designation/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult ManageStore()
        {

            var _temp = ClientSession.CompanyID;
            var _temp1 = ClientSession.EmployeeID;

            StoreModel mod = new StoreModel();
            return View(mod);
        }


        public JsonResult LoadDataForStoreDataTable()
        {
            try
            {

                IEnumerable<Store_SelectByCompanyID_Result> streLst = StoreList();
                

                int recordsTotal = 0;
                //var storeData = (from tempStore in db.Stores
                //                 select tempStore);
                //storeData.Where(a => a.companyId == 1);

                recordsTotal = streLst.Count();
                var data = streLst.ToList();
                return Json(new { Result = "OK", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public string GetStoreDataByStoreID(int storeID)
        {
            int _compID = ClientSession.CompanyID;
            string data = JsonConvert.SerializeObject(db.Store_SelectByStoreID(storeID, _compID));
            return "" + data;
        }



        [HttpPost]
        public Boolean DelteStore(int storeID)
        {
            Store objStore = db.Stores.Where(o => o.storeID == storeID).SingleOrDefault();
            objStore.active = false;
            db.SaveChanges();
            return true;
        }

        public IEnumerable<POSEntity.Store_SelectByCompanyID_Result> StoreList()
        {
            return db.Store_SelectByCompanyID(ClientSession.CompanyID).ToList();
        }


        public ActionResult AddEditStore(String id)
        {
            int _companyid = ClientSession.CompanyID;
            Random rand = new Random();

            int _newValue = rand.Next();
            
            StoreModel mod = new StoreModel();
            Store objStore;
            int _hdnStoreId = Convert.ToInt32(id);
            if (_hdnStoreId > 0)
            {

                objStore = db.Stores.Where(o => o.storeID == _hdnStoreId).SingleOrDefault();
                mod.storeID = _hdnStoreId;
                mod.active = Convert.ToBoolean(objStore.active);
                mod.storeCode = objStore.storeCode;
            }
            else
            {
                objStore = new Store();
                mod.active = true;
                mod.storeCode =  _companyid+"-" +_newValue;
                mod.companyId = _companyid;
            }
            
            
            
            mod.storeName = objStore.storeName;
            mod.address = objStore.address;
            mod.phoneNo = objStore.phone;
            mod.servicestartDate = Convert.ToDateTime(objStore.servicestartDate).ToShortDateString() == "1/1/0001" ? "" : Convert.ToDateTime(objStore.servicestartDate).ToShortDateString();
            mod.terminationDate = Convert.ToDateTime(objStore.terminationDate).ToShortDateString() == "1/1/0001" ? "" : Convert.ToDateTime(objStore.terminationDate).ToShortDateString();
            

            return View(mod);


        }


        [HttpPost]
        public ActionResult AddEditStoreSave(FormCollection frm, HttpPostedFileBase logoUpload)
        {
            int _companyid = ClientSession.CompanyID;
            int _hdnStoreId = 0;
            String _isActive = "";
            Store objStore;
            String n1 = _hdnStoreId.ToString();


            if (ModelState.IsValid)
            {
                _hdnStoreId = Convert.ToInt32(frm["hdnStoreId"].ToString());

                if (_hdnStoreId > 0)
                {

                    objStore = db.Stores.Where(o => o.storeID == _hdnStoreId).SingleOrDefault();


                }
                else
                {
                    objStore = new Store();
                  
                    objStore.storeCode = frm["storeCode"].ToString();
                    objStore.companyId = _companyid;
                }



                
                _isActive = frm["active"].ToString();
                if (_isActive == "true,false" || _isActive == "true")
                {
                    objStore.active = true;
                    objStore.terminationDate = null;

                }
                else
                {

                    objStore.active = false;
                    objStore.terminationDate = Convert.ToDateTime(frm["terminationDate"]);

                }

                objStore.storeName = frm["storeName"].ToString();
                objStore.servicestartDate = Convert.ToDateTime(frm["servicestartDate"]);
                objStore.address = frm["address"];
                objStore.phone = frm["phoneNo"];
                

                if (_hdnStoreId > 0)
                {
                    objStore.updatedBy = 1;
                    objStore.updatedDate = DateTime.Now;
                    db.Entry(objStore).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    objStore.createdBy = 1233;
                    objStore.createdDate = DateTime.Now;
                    db.Stores.Add(objStore);
                }

                db.SaveChanges();




            }

            //Users/LoadDataForDataTable
            return RedirectToAction("ManageStore");

            //return View("AddEditUserSave");
        }



        [HttpGet]
        public String ValidateStoreName(String storeName,String storeid)
        {
            String _retval = "0";
            int _companyId = ClientSession.CompanyID;
            int _streD = int.Parse(storeid);
            Store objStore = db.Stores.Where(o => o.storeName == storeName.Trim().TrimStart().TrimEnd() && o.companyId == _companyId).SingleOrDefault();
            if(objStore!=null && objStore.storeID > 0)
            {

                _retval = "1";
            }
            //db.SaveChanges();
            return _retval;
        }
        



    }
}