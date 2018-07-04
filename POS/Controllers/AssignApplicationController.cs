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
using System.Web.Script.Serialization;

namespace POS.Controllers
{
    public class AssignApplicationController : Controller
    {
        //
        // GET: /Rate/
        onlineinternetposEntities db = new onlineinternetposEntities();
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ManageAssignApplication(string selectedItems, string selectedType, string selectedNameid)
        {

            int _companyID = 1;
            int _desigid = 0;
            int _empid = 0;
            int _getCompanyMapid = 0;
             int _menuid = 0;
             String _menuids = "";
             MenuAuthorization objMenuAuth;

            if (selectedType.ToLower() == "designation")
            {
                _desigid = Convert.ToInt32(selectedNameid);
            }
            else
            {
                _empid = Convert.ToInt32(selectedNameid);
            }
             
            List<TreeViewNode> items = (new JavaScriptSerializer()).Deserialize<List<TreeViewNode>>(selectedItems);
            foreach (var itemsVal in items)
            {
                _menuids += itemsVal.id + ",";
            //    _menuid = Convert.ToInt32(itemsVal.id);
            //    _getCompanyMapid = getAppCompanyMappid(_menuid, _companyID);
            //    objMenuAuth = new MenuAuthorization();
            //    objMenuAuth.employeeID = 0;
            //    objMenuAuth.designationID = 1;
            //    objMenuAuth.appCompanyMapID = 2;
            //    objMenuAuth.isDashboard = false;
            //    objMenuAuth.imageURL = "";
            //    db.MenuAuthorizations.Add(objMenuAuth);
            //    db.SaveChanges();
            }

            _menuids.TrimEnd(',').TrimStart().TrimEnd();

            int _retVal=db.DELETEMenuAuthorizationByEmpIDAndDesigID(_companyID, _menuids, _desigid, _empid);
            //String _returnURL = "ManageAssignApplication?seltype=" + selectedType + "&selName=" + selectedNameid;
            return this.RedirectToAction("ManageAssignApplication", new { seltype = selectedType, selName = selectedNameid });
            //Redirect(_returnURL);
            //return RedirectToAction("ManageAssignApplication");
        }

        public int getAppCompanyMappid(int menuid, int companyid)
        {

            int _appCompanyMapid = 0;
            var resultsappCompanyMap = from m in db.Menu_List
                                       join map in db.MenuAppCompanyMapps on m.menuID equals map.menuID
                                       where (map.companyID == companyid) && (map.menuID == menuid)
                                       select new { appCompanyMapID = map.appCompanyMapID };

            var lstappCompanyMap = resultsappCompanyMap.ToList(); //db.Menu_List.ToList();
            foreach (var s in lstappCompanyMap)
            {
                _appCompanyMapid = s.appCompanyMapID;
            }

            return _appCompanyMapid;

        }


        [HttpGet]
        public ActionResult ManageAssignApplication(string seltype, string selName)
        {
            int _companyID = 1;
            int _desID = 0;
            int _empID = 0;
            String _qry = "";
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            var results = from m in db.Menu_List
                          join map in db.MenuAppCompanyMapps on m.menuID equals map.menuID
                          join priv in db.MenuApplicationPrivacies on m.menuID equals priv.menuID
                          where (map.companyID == _companyID) && (priv.CompanyID == _companyID)
                          select new { menuID = m.menuID, menuParentID = m.menuParentID ,menuName= m.menuName, controllerName=m.controllerName, actionName =m.actionName};//new { country = cn.name, city = ct.name, c.id, c.name, c.address1, c.address2, c.address3, c.countryid, c.cityid, c.region, c.postcode, c.telephone, c.website, c.sectorid, status = (contactstatus)c.statusid, sector = sect.name };


            var lst = results.ToList(); //db.Menu_List.ToList();
            foreach (var type in lst)
            {
                if (type.menuParentID == 0)
                {
                    nodes.Add(new TreeViewNode { id = type.menuID.ToString(), parent = "#", text = type.menuName });
                }
                else
                {
                    //nodes.Add(new TreeViewNode { id = type.menuParentID.ToString() + "-" + type.menuID.ToString(), parent = type.menuParentID.ToString(), text = type.menuName });

                    nodes.Add(new TreeViewNode { id = type.menuID.ToString(), parent = type.menuParentID.ToString(), text = type.menuName });

                }

            }
            //Serialize to JSON string.
            ViewBag.Json = (new JavaScriptSerializer()).Serialize(nodes);

            String _selectedMenuVal = String.Empty;
            if (!String.IsNullOrEmpty(seltype) && !String.IsNullOrEmpty(selName))
            {
            
            if(seltype=="Designation")
            {
                _empID = 0;
                _desID = Convert.ToInt32(selName);
                
            }
            else
            {
                _desID = 0;
                _empID = Convert.ToInt32(selName);
                
            }


            var lstMenuBydes = db.GetMenuBydesignationOrEmployeeID(_desID, _empID, _companyID).ToList();
            foreach (var d in lstMenuBydes)
            {
                _selectedMenuVal += "'" + d.id + "'" + ",";
            }

            _selectedMenuVal = "[" + _selectedMenuVal.TrimEnd(',') + "]";

            //JsonResult result = new JsonResult();
            //result.Data = lstMenuBydes;
            //result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //return result;
            //ViewBag.Json1 = result;
            ViewBag.Json1 = _selectedMenuVal; //"['8','10']";


            ViewBag.QueryStringSeltype = seltype;
            ViewBag.QueryStringSelName = selName;

                //ViewBag.Json1 = "['8','10']";
                //ViewBag.Json1  = (new JavaScriptSerializer()).Serialize(lstMenuBydes); 

            //ViewBag.Json = (new JavaScriptSerializer()).Serialize(lstMenuBydes);
                }
            return View();

            //AssignApplicationModel mod = new AssignApplicationModel();
            //return View(mod);

        }

        [HttpPost]
        public JsonResult Type_Selected(string id)
        {

            IEnumerable<POSEntity.GetDesignationAndEmpdsForAssignApplication_Result> rateLst = RateList(id);
            JsonResult result = new JsonResult();
            result.Data = rateLst;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }


         public IEnumerable<POSEntity.GetDesignationAndEmpdsForAssignApplication_Result> RateList(String id)
        {
         
             return db.GetDesignationAndEmpdsForAssignApplication(id, 1);
        }

        [HttpGet]
        public string GetRateDataByRateID(int rateID)
        {
            string data = JsonConvert.SerializeObject(db.Rate_SelectByRateID(rateID));
            return "" + data;
        }


        [HttpPost]
        public ActionResult AddEditAssignApplication(string selectedItems)
        {
            List<TreeViewNode> items = (new JavaScriptSerializer()).Deserialize<List<TreeViewNode>>(selectedItems);
            return RedirectToAction("Index");
        }



     //     [HttpPost]
     //   public ActionResult OnLoadSelectAssignApplication(string seltype, string selName)
     //   {
     //       String _selectedMenuVal = String.Empty;
     //       int _empid = 0;
     //       int _desid = 0;
     //       int _compid = 1;
             
     //if(seltype=="Designation")
     //{
     //    _desid = Convert.ToInt32(selName);
     //}
     //         else
     //{

     //    _empid = Convert.ToInt32(selName);
     //}

     //var lstMenuBydes = db.GetMenuBydesignationOrEmployeeID(_desid, _empid, _compid).ToList();
     //       foreach (var d in lstMenuBydes)
     //       {
     //           _selectedMenuVal += "'" + d.menuID + "'" + ",";
     //       }

     //       JsonResult result = new JsonResult();
     //       result.Data = _selectedMenuVal;
     //       result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
           

     //       _selectedMenuVal = "[" + _selectedMenuVal.TrimEnd(',') + "]";
     //       ViewBag.Json1 = _selectedMenuVal; //"['8','10']";
     //       Session["selectedMenuVal"] = _selectedMenuVal;

     //       return result;

     //       //return RedirectToAction("ManageAssignApplication"); 
     //   }
     
    }
}
