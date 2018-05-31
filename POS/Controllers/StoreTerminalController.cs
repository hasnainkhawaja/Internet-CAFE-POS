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


            #region loadstores
            List<Floor> lstFloor = (from data in db.Floors
                                  select data).ToList();

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
            mod.color = objStoreTerminal.color;

            return View(mod);


        }


        [HttpPost]
        public ActionResult AddEditStoreTerminalSave(FormCollection frm, HttpPostedFileBase logoUpload)
        {
            int _companyid = ClientSession.CompanyID;
            int _hdnterminalID = 0;
            String _isActive = "";
            Store objStore;
            Guid objGuid = Guid.NewGuid();
            
            StoreTerminalModel mod = new StoreTerminalModel();
            Store_Terminal objStoreTerminal;
            //String n1 = _hdnStoreId.ToString();


            if (ModelState.IsValid)
            {
                _hdnterminalID = Convert.ToInt32(frm["hdnterminalID"].ToString());

                if (_hdnterminalID > 0)
                {

                    objStoreTerminal = db.Store_Terminal.Where(o => o.terminalID == _hdnterminalID).SingleOrDefault();
                    mod.terminalID = _hdnterminalID;
                    
                }
                else
                {
                    objStoreTerminal = new Store_Terminal();
                    objStoreTerminal.connectionCode = objGuid;
                    
                }

                _isActive = frm["active"].ToString();
                if (_isActive == "true,false" || _isActive == "true")
                {
                    objStoreTerminal.active = true;
                    

                }
                else
                {
                    objStoreTerminal.active = false;   
                }


                objStoreTerminal.terminalCode = frm["terminalCode"].ToString();
                objStoreTerminal.title =  frm["title"].ToString();
                //objStoreTerminal.connectionCode = frm["connectionCode"].ToString();
                objStoreTerminal.floorid = Convert.ToInt32(frm["SelectedFloorId"].ToString());
                objStoreTerminal.color = frm["color"].ToString();



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

            //Users/LoadDataForDataTable
            return RedirectToAction("ManageStoreTerminal");

            //return View("AddEditUserSave");
        }


        [HttpGet]
        public String ValidateTerminalName(String terminalName , String terminalID)
        { 
            String _retval = "0";
            int? _companyId=1;
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