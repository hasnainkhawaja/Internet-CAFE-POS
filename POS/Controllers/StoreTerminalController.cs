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
    public class StoreTerminalController : Controller
    {
        int _hdnStoreterminalId = 0;
        onlineinternetposEntities db = new onlineinternetposEntities();

        //
        // GET: /Designation/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult ManageStoreTerminal()
        {
            StoreTerminalModel mod = new StoreTerminalModel();
            return View(mod);
        }


        public JsonResult LoadDataForStoreDataTable()
        {
            try
            {

                IEnumerable<Store_Terminal_ByCompanyID_Result> streLst = StoreList();
                

                int recordsTotal = 0;
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

        public IEnumerable<POSEntity.Store_Terminal_ByCompanyID_Result> StoreList()
        {
            return db.Store_Terminal_ByCompanyID(ClientSession.CompanyID).ToList();
        }


        public ActionResult AddEditStoreTerminal(String id)
        {
            StoreTerminalModel mod = new StoreTerminalModel();
            Store_Terminal objStoreTerminal;
            _hdnStoreterminalId = Convert.ToInt32(id);

            int _companyid = ClientSession.CompanyID;
            Random rand = new Random();

            int _newValue = rand.Next();


            #region loadfloors
            List<Floor> lstFloor = db.SelectFloors(ClientSession.CompanyID, -2).ToList().Select(x => new Floor() { Floorid = x.Floorid, Storeid = x.Storeid, Active = x.Active, Title = x.Title }).ToList();

            Floor objFloor = new Floor();
            objFloor.Title = "Select";
            objFloor.Floorid = 0;
            lstFloor.Insert(0, objFloor);
            mod.Floor = lstFloor.Select(a => new SelectListItem
            {
                Text = a.Title,
                Value = a.Floorid.ToString()
            });
            #endregion

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

            
            if (_hdnStoreterminalId > 0)
            {

                objStoreTerminal = db.Store_Terminal.Where(o => o.terminalID == _hdnStoreterminalId).SingleOrDefault();
                mod.terminalID = _hdnStoreterminalId;
                mod.terminalCode = objStoreTerminal.terminalCode;
            }
            else
            {
                objStoreTerminal = new Store_Terminal();
                mod.active = true;
                mod.terminalCode = _companyid + "-" + _newValue;     
            }


            mod.active = Convert.ToBoolean(objStoreTerminal.active);
            mod.title = objStoreTerminal.title;
            mod.connectionCode =  objStoreTerminal.connectionCode;
            mod.SelectedFloorId = Convert.ToInt32(objStoreTerminal.floorid);
            var floorval = db.Floors.SingleOrDefault(x => x.Floorid == objStoreTerminal.floorid);
            mod.SelectedStoreId  = floorval!=null?floorval.Storeid.Value :0;
            mod.color = objStoreTerminal.color;

            return View(mod);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditStoreTerminal(StoreTerminalModel model)
        {
            int _companyid = ClientSession.CompanyID;
            int _hdnterminalID = 0;
            String _isActive = ""; 
            Guid objGuid = Guid.NewGuid();
            
            StoreTerminalModel mod = new StoreTerminalModel();
            Store_Terminal objStoreTerminal;
            //String n1 = _hdnStoreId.ToString();


            if (ModelState.IsValid)
            {
                var result = ValidateTerminalName(model.title, mod.terminalID.HasValue == false ? "" : mod.terminalID.Value.ToString());
                if (result.ToString() == "1")
                {
                    ModelState.AddModelError("title", "Terminal title already in user"); 
                }
                else
                {

                    if (mod.terminalID > 0)
                    {

                        objStoreTerminal = db.Store_Terminal.Where(o => o.terminalID == mod.terminalID).SingleOrDefault();

                    }
                    else
                    {
                        objStoreTerminal = new Store_Terminal();
                        objStoreTerminal.connectionCode = objGuid;
                    }

                    _isActive = model.active.ToString();
                    if (_isActive == "true,false" || _isActive == "true")
                    {
                        objStoreTerminal.active = true;

                    }
                    else
                    {
                        objStoreTerminal.active = false;
                    }
                     
                    objStoreTerminal.terminalCode = model.terminalCode.ToString();
                    objStoreTerminal.title = model.title.ToString(); 
                    objStoreTerminal.floorid = Convert.ToInt32(model.SelectedFloorId);
                    objStoreTerminal.color = model.color;

                    if (_hdnterminalID > 0)
                    {
                        objStoreTerminal.updatedBy = 1;
                        objStoreTerminal.updatedDated = DateTime.Now;
                        db.Entry(objStoreTerminal).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        objStoreTerminal.createdBy = 1233;
                        objStoreTerminal.createdDate = DateTime.Now;
                        db.Store_Terminal.Add(objStoreTerminal);
                    }

                    db.SaveChanges(); 

                }
                 
                return RedirectToAction("ManageStoreTerminal");
            }
             

            #region loadfloors
            List<Floor> lstFloor = db.SelectFloors(ClientSession.CompanyID, -1).ToList().Select(x=>new Floor(){ Floorid = x.Floorid , Storeid = x.Storeid , Active = x.Active , Title = x.Title}).ToList();

            Floor objFloor = new Floor();
            objFloor.Title = "Select";
            objFloor.Floorid = 0;
            lstFloor.Insert(0, objFloor);
            model.Floor = lstFloor.Select(a => new SelectListItem
            {
                Text = a.Title,
                Value = a.Floorid.ToString()
            });
            #endregion

            #region loadstores
            List<Store> stores = (from data in db.Stores
                                  where data.companyId == ClientSession.CompanyID
                                  select data).ToList();

            Store objStores = new Store();
            objStores.storeName = "Select";
            objStores.storeID = 0;
            stores.Insert(0, objStores);
            model.Stores = stores.Select(a => new SelectListItem
            {
                Text = a.storeName,
                Value = a.storeID.ToString()
            });
            #endregion

            return View(model);
        }


        [HttpGet]
        public String ValidateTerminalName(String terminalName , String terminalID)
        { 
            String _retval = "0";
            int? _companyId=ClientSession.CompanyID;
            int _terminalID = 0;

             if(!String.IsNullOrEmpty(terminalID))
             {

                 _terminalID = Convert.ToInt32(terminalID);
             }


            IEnumerable<ValidateStoreTerminal_Result> valLst = db.ValidateStoreTerminal(_terminalID, terminalName, _companyId).ToList();


              if (valLst != null && valLst.Count() >0)
            {
                _retval = "1";
            }
            
            return _retval;
        }
        



    }
}