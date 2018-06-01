using Newtonsoft.Json;
using POS.Helpers;
using POS.Models;
using POSEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Controllers
{
    public class CategoryController : Controller
    {
        //
         
        onlineinternetposEntities db = new onlineinternetposEntities();
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult ManageCategory()
        {
            return View(); 
        }



        public JsonResult LoadDataForCategoryDataTable()
        {
            try
            {

                IEnumerable<Category_SelectByCompanyID_Result> rateLst = CategoriesList();


                int recordsTotal = 0;
                 

                recordsTotal = rateLst.Count();
                var data = rateLst.ToList();
                return Json(new { Result = "OK", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        private IEnumerable<POSEntity.Category_SelectByCompanyID_Result> CategoriesList()
        {
            return db.Category_SelectByCompanyID(ClientSession.CompanyID).ToList();
        }



        [HttpGet]
        public string GetRateDataByRateID(Int64 categoryId)
        {
            string data = JsonConvert.SerializeObject(db.Categories.SingleOrDefault(x => x.categoryId == categoryId && x.companyid==ClientSession.CompanyID));
            return "" + data;
        }


        [HttpPost]
        public Boolean DeleteCategory(Int64 categoryId)
        {
            Category objrate = db.Categories.Where(o => o.categoryId == categoryId && o.companyid == ClientSession.CompanyID).SingleOrDefault();
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
        public ActionResult AddEditCategory(Int64 id=0)
        {
            int _companyid = ClientSession.CompanyID;

            CategoryModel mod = new CategoryModel();

            #region loadstores
            List<Store> stores = (from data in db.Stores
                                  where data.companyId == ClientSession.CompanyID
                                  select data).ToList();

            Store objStores = new Store();
            objStores.storeName = "All Stores";
            objStores.storeID = 0;
            stores.Insert(0, objStores);
            mod.Stores = stores.Select(a => new SelectListItem
            {
                Text = a.storeName,
                Value = a.storeID.ToString()
            });
            #endregion

            #region loadcategories
            List<POSEntity.Category> categories = (from data in db.Categories where data.companyid==ClientSession.CompanyID && data.categoryId!=id
                                              select data).ToList();

            POSEntity.Category objcategoryType = new POSEntity.Category();
            objcategoryType.categoryTitle = "Main Category";
            objcategoryType.categoryId = 0;
            categories.Insert(0, objcategoryType);
            mod.Categories = categories.Select(a => new SelectListItem
            {
                Text = a.categoryTitle,
                Value = a.categoryId.ToString()
            });
            #endregion

            Category objcategory;
            Int64 _hdnrateId = Convert.ToInt64(id);
            if (_hdnrateId > 0)
            {

                objcategory = db.Categories.Where(o => o.categoryId == _hdnrateId && o.companyid == ClientSession.CompanyID).SingleOrDefault();
                if (objcategory != null)
                {
                    mod.categoryId = _hdnrateId;
                    mod.CategoryTilte = objcategory.categoryTitle;
                    mod.SelectedStoreId = objcategory.storeid.Value;
                    mod.companyid = objcategory.companyid.Value;
                    mod.CategoryCode = objcategory.categoryCode;
                    mod.appliesDiscount = objcategory.appliesDiscount.Value;
                    mod.printOrder = objcategory.printOrder.Value;
                    mod.isActive = objcategory.active.Value;
                    mod.SelectedParentCategoryId = objcategory.parentId.Value; 
                }
                else
                {
                    throw new HttpException(404, "Rate does not exists");
                }
            }
            else
            {
                mod.SelectedParentCategoryId = 0;
            }
             
            return View(mod);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditCategory(CategoryModel mod)
        {

            Category objCategory;

            if (ModelState.IsValid)
            {

                if (mod.categoryId > 0)
                {
                    objCategory = db.Categories.Where(o => o.categoryId == mod.categoryId).SingleOrDefault();
                    if (objCategory == null)
                    {
                        throw new HttpException(404, "Rate does not exists");
                    }
                }
                else
                {
                    objCategory = new Category();
                }

                objCategory.categoryTitle = mod.CategoryTilte;
                objCategory.categoryCode = mod.CategoryCode;
                objCategory.companyid = ClientSession.CompanyID;
                objCategory.storeid = mod.storeid;

                objCategory.printOrder = mod.printOrder;
                objCategory.appliesDiscount = mod.appliesDiscount;
                objCategory.active = mod.isActive;
                objCategory.parentId = mod.SelectedParentCategoryId; 
                objCategory.storeid = mod.SelectedStoreId;

                if (mod.categoryId > 0)
                {
                    objCategory.updaedBy = 1;
                    objCategory.updatedDate = DateTime.Now;
                    db.Entry(objCategory).State = EntityState.Modified;

                    db.SaveChanges();
                }
                else
                {
                    objCategory.createdBy = 1233;
                    objCategory.createdDate = DateTime.Now;
                    db.Categories.Add(objCategory);
                }

                db.SaveChanges();

                //Users/LoadDataForDataTable
                return RedirectToAction("ManageCategory");
            }




            #region loadstores
            List<Store> stores = (from data in db.Stores
                                  where data.companyId == ClientSession.CompanyID
                                  select data).ToList();

            Store objStores = new Store();
            objStores.storeName = "All Stores";
            objStores.storeID = 0;
            stores.Insert(0, objStores);
            mod.Stores = stores.Select(a => new SelectListItem
            {
                Text = a.storeName,
                Value = a.storeID.ToString()
            });
            #endregion

            #region loadcategories
            List<POSEntity.Category> categories = (from data in db.Categories
                                                   where data.companyid == ClientSession.CompanyID
                                                   && data.categoryId!=mod.categoryId
                                                   select data).ToList();

            POSEntity.Category objcategoryType = new POSEntity.Category();
            objcategoryType.categoryTitle = "Main Category";
            objcategoryType.categoryId = 0;
            categories.Insert(0, objcategoryType);
            mod.Categories = categories.Select(a => new SelectListItem
            {
                Text = a.categoryTitle,
                Value = a.categoryId.ToString()
            });
            #endregion


            return View(mod);
        }
         
        [HttpPost]
        public ActionResult ValidateCategory(string CategoryCode,Int64 categoryid=0)
        {

            if (db.Categories.Where(x => x.categoryCode.Equals(CategoryCode) && x.companyid == ClientSession.CompanyID && x.categoryId != categoryid).Count() > 0)
            {
                return Json(false);
            }

            return Json(true); 
        }


        [HttpPost]
        public JsonResult LoadDataForStoreCateogriesDataTable(Int64 storeid,Int64 categoryid=0)
        {
            try
            {

                IEnumerable<Category> streLst = db.Categories.Where(x =>(x.storeid == storeid || x.storeid==0) && x.categoryId!=categoryid).ToList();


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
    }
}
