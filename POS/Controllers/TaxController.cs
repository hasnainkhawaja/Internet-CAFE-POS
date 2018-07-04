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
    public class TaxController : Controller
    {
        //
        // GET: /Rate/
        onlineinternetposEntities db = new onlineinternetposEntities();
        


        public ActionResult ManageTax()
        { 
            return View();

        }

         

        public JsonResult LoadDataForTaxDataTable()
        {
            try
            {

                IEnumerable<TaxCategory_SelectByCompanyID_Result> rateLst = CategoryList();


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

        public IEnumerable<POSEntity.TaxCategory_SelectByCompanyID_Result> CategoryList()
        {
            return db.TaxCategory_SelectByCompanyID(ClientSession.CompanyID).ToList();
        }



        [HttpGet]
        public string GetRateDataByRateID(int rateID)
        {
            string data = JsonConvert.SerializeObject(db.Rate_SelectByRateID(rateID));
            return "" + data;
        }


        [HttpPost]
        public Boolean DeleteRate(int taxCategoryID)
        {
            var  objTax = db.TaxCategories.Where(o => o.id == taxCategoryID && o.companyid == ClientSession.CompanyID).SingleOrDefault();
            if (objTax == null)
            {
                return false;
            }
            else
            {
                objTax.active = false;
                db.SaveChanges();
                return true;
            }
        }


        [HttpGet]
        public ActionResult AddEditTax(String id)
        {
            int _companyid = ClientSession.CompanyID;

            TaxCategoryModel mod = new TaxCategoryModel();

            

            POSEntity.TaxCategory objTax;
            int _hdnrateId = Convert.ToInt32(id);
            if (_hdnrateId > 0)
            {

                objTax = db.TaxCategories.Where(o => o.id == _hdnrateId && o.companyid == ClientSession.CompanyID).SingleOrDefault();
                if (objTax != null)
                {
                    mod.active = objTax.active.Value;
                    mod.companyID = objTax.companyid.Value;
                    mod.title = objTax.title;
                    mod.rate = objTax.rate.Value;
                    mod.taxid = objTax.id;
                }
                else
                {
                    throw new HttpException(404, "Rate does not exists");
                }
            }
            else
            {
                mod.companyID = ClientSession.CompanyID;
            }  

            return View(mod);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditTax(TaxCategoryModel tax)
        {

            TaxCategory objTax;

            if (ModelState.IsValid)
            {
                 
                    if (tax.taxid > 0)
                    {
                        objTax = db.TaxCategories.Where(o => o.id == tax.taxid && o.companyid == ClientSession.CompanyID).SingleOrDefault();
                        if (objTax == null)
                        {
                            throw new HttpException(404, "Tax Category does not exists");
                        }
                    }
                    else
                    {
                        objTax = new TaxCategory();
                    }

                    objTax.title = tax.title;
                    objTax.rate = tax.rate;
                    objTax.active = tax.active;
                    objTax.id = tax.taxid;
                    objTax.companyid = ClientSession.CompanyID;

                    if (tax.taxid > 0)
                    {

                        db.Entry(objTax).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                    else
                    { 
                        db.TaxCategories.Add(objTax);
                    }

                    db.SaveChanges();
                     
                    //Users/LoadDataForDataTable
                    return RedirectToAction("ManageTax");
                 
            }
             


            return View(tax);
        }
    }
}
