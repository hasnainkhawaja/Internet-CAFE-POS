//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using POSEntity;
//using POS.Models;
//using Newtonsoft./Json;
//using System.Data.Entity;
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
    public class RateController : Controller
    {
        //
        // GET: /Rate/
        onlineinternetposEntities db = new onlineinternetposEntities();
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult ManageRate()
        {
            RateModel mod = new RateModel();
            return View(mod);

        }

         

        public JsonResult LoadDataForRateDataTable()
        {
            try
            {

                IEnumerable<Rate_SelectByCompanyID_Result> rateLst = RateList();


                int recordsTotal = 0;
                //var storeData = (from tempStore in db.Stores
                //                 select tempStore);
                //storeData.Where(a => a.companyId == 1);

                recordsTotal = rateLst.Count();
                var data = rateLst.ToList();
                return Json(new { Result = "OK", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<POSEntity.Rate_SelectByCompanyID_Result> RateList()
        {
            return db.Rate_SelectByCompanyID(ClientSession.CompanyID).ToList();
        }



        [HttpGet]
        public string GetRateDataByRateID(int rateID)
        {
            string data = JsonConvert.SerializeObject(db.Rate_SelectByRateID(rateID));
            return "" + data;
        }


        [HttpPost]
        public Boolean DeleteRate(int rateID)
        {
            Rate objrate = db.Rates.Where(o => o.rateID == rateID &&  o.companyID==ClientSession.CompanyID).SingleOrDefault();
            if (objrate == null)
            {
                return false;
            }
            else
            {
                objrate.active = false;
                db.SaveChanges();
                return true;
            }
        }


        [HttpGet]
        public ActionResult AddEditRate(String id)
        {
            int _companyid = ClientSession.CompanyID;

            RateModel mod = new RateModel();

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

            #region loadRates
            List<POSEntity.RateType> rates = (from data in db.RateTypes
                                              select data).ToList();

            POSEntity.RateType objRateType = new POSEntity.RateType();
            objRateType.Title = "Select";
            objRateType.RateTypeid = 0;
            rates.Insert(0, objRateType);
            mod.RateTypes = rates.Select(a => new SelectListItem
            {
                Text = a.Title,
                Value = a.RateTypeid.ToString()
            });
            #endregion

            Rate objRate;
            int _hdnrateId = Convert.ToInt32(id);
            if (_hdnrateId > 0)
            {

                objRate = db.Rates.Where(o => o.rateID == _hdnrateId && o.companyID==ClientSession.CompanyID).SingleOrDefault();
                if (objRate != null)
                {
                    mod.startTime = Convert.ToDateTime(objRate.startTime).ToShortTimeString();
                    mod.endTime = Convert.ToDateTime(objRate.endTime).ToShortTimeString();
                    mod.SelectedStoreId = Convert.ToInt32(objRate.storeID);
                    mod.SelectedRateId = Convert.ToInt32(objRate.ratetype);
                    mod.rateCode = objRate.rateCode;
                }
                else
                {
                    throw new HttpException(404, "Rate does not exists");
                }
            }
            else
            {
                objRate = new Rate();
                mod.startTime = DateTime.Now.ToShortTimeString();
                mod.endTime = DateTime.Now.ToShortTimeString();

            }
            //PopulateStoresDropDownList(mod.storeID);
            mod.rateID = _hdnrateId;

            mod.title = objRate.title;
            mod.amount = Convert.ToDecimal(objRate.amount);
            mod.color = objRate.color;
            mod.bufferTime = Convert.ToInt32(objRate.bufferTime);
            mod.alertInterval = Convert.ToInt32(objRate.alertInterval); 

            mod.isPrepay = Convert.ToBoolean(objRate.isPrepay);
            mod.active = Convert.ToBoolean(objRate.active);


            return View(mod);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditRate(RateModel rate)
        {

            Rate objRate;

            if (ModelState.IsValid)
            {
                if (rate.SelectedRateId == 2 && rate.alertInterval <= 0)
                {
                    ModelState.AddModelError("alertInterval", "Enter value greater than zero for deal rates");
                }
                else
                {
                    if (rate.rateID > 0)
                    {
                        objRate = db.Rates.Where(o => o.rateID == rate.rateID && rate.companyID==ClientSession.CompanyID).SingleOrDefault();
                        if(objRate==null)
                        {
                            throw new HttpException(404, "Rate does not exists");
                        }
                    }
                    else
                    {
                        objRate = new Rate();
                    }

                    objRate.startTime = Convert.ToDateTime(rate.startTime);
                    objRate.endTime = Convert.ToDateTime(rate.endTime);
                    objRate.rateCode = rate.rateCode;
                    objRate.title = rate.title;

                    objRate.amount = rate.amount;
                    objRate.color = rate.color;
                    objRate.bufferTime = rate.bufferTime;
                    objRate.alertInterval = rate.alertInterval;
                    objRate.storeID = rate.SelectedStoreId;
                    objRate.ratetype = rate.SelectedRateId;
                    objRate.active = rate.active;
                    objRate.isPrepay = rate.isPrepay;
                    objRate.companyID = ClientSession.CompanyID;

                    if (objRate.rateID > 0)
                    {
                        objRate.updatedBy = 1;
                        objRate.updatedDate = DateTime.Now;
                        db.Entry(objRate).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                    else
                    {
                        objRate.createdBy = 1233;
                        objRate.Createddate = DateTime.Now;
                        db.Rates.Add(objRate);
                    }

                    db.SaveChanges();
                     
                    //Users/LoadDataForDataTable
                    return RedirectToAction("ManageRate");
                }
            }



            #region loadstores
            List<Store> stores = (from data in db.Stores where data.companyId== ClientSession.CompanyID
                                  select data).ToList();

            Store objStores = new Store();
            objStores.storeName = "Select";
            objStores.storeID = 0;
            stores.Insert(0, objStores);
            rate.Stores = stores.Select(a => new SelectListItem
            {
                Text = a.storeName,
                Value = a.storeID.ToString()
            });
            #endregion

            #region loadRates
            List<POSEntity.RateType> rates = (from data in db.RateTypes
                                              select data).ToList();

            POSEntity.RateType objRateType = new POSEntity.RateType();
            objRateType.Title = "Select";
            objRateType.RateTypeid = 0;
            rates.Insert(0, objRateType);
            rate.RateTypes = rates.Select(a => new SelectListItem
            {
                Text = a.Title,
                Value = a.RateTypeid.ToString()
            });
            #endregion


            return View(rate);
        }
    }
}
