using POS.Helpers;
using POS.Models;
using POSEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/ 
        onlineinternetposEntities db = new onlineinternetposEntities();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ManageProduct()
        {
            return View();
        }


        public JsonResult LoadDataForProductDataTable(Int64 storeid=0)
        {
            if(storeid==0)
            {
                return Json(new { Result = "OK", recordsFiltered = 0, recordsTotal = 0, data = new  List<Product_SelectByCompanyAndStore_Result>() });
            }
            else
            {
                try
                {

                    IEnumerable<Product_SelectByCompanyAndStore_Result> productLst = PrductList(ClientSession.CompanyID, storeid);


                    int recordsTotal = 0;


                    recordsTotal = productLst.Count();
                    var data = productLst.ToList();
                    return Json(new { Result = "OK", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

                }
                catch (Exception)
                {
                    throw;
                }
            } 

        }

        private IEnumerable<POSEntity.Product_SelectByCompanyAndStore_Result> PrductList(int companyid,Int64 storeid)
        {
            return db.Product_SelectByCompanyAndStore(companyid, storeid).ToList();
        }


        [HttpPost]
        public ActionResult ValidateProductBarcode(string barcode, Int64? productid)
        {

            if (!productid.HasValue)
            {
                productid = 0;
            }

            if (db.ValidateProductBarcode(ClientSession.CompanyID, barcode, productid).First().Value > 0)
            {
                return Json(false);
            }

            return Json(true);
        }

        [HttpPost]
        public ActionResult ValidateProductCode(string productCode, Int64? productid)
        {

            if (!productid.HasValue)
            {
                productid = 0;
            }

            if (db.ValidateProductCode(ClientSession.CompanyID, productCode, productid).First().Value > 0)
            {
                return Json(false);
            }

            return Json(true);
        }



        [HttpGet]
        public ActionResult AddEditProduct(Int64 id = 0)
        {
            int _companyid = ClientSession.CompanyID;

            #region loadsettings
            ProductModel mod = new ProductModel();
            List<Category> categories = db.Categories.Where(x => x.companyid == _companyid).ToList();
            List<BarcodeType> barcodeType = db.BarcodeTypes.Where(x => x.active==true).ToList();
            List<ProductUnit> productUnit = db.ProductUnits.Where(x => x.active == true).ToList();
            List<TaxCategory> TaxUnit = db.TaxCategories.Where(x => x.active == true && x.companyid== ClientSession.CompanyID).ToList();

           
                
            mod.Categories = categories.Select(a => new SelectListItem
            {
                Text = a.categoryCode+"-"+a.categoryTitle,
                Value = a.categoryId.ToString()
            });

            mod.BarcodeTypes = barcodeType.Select(a => new SelectListItem
            {
                Text = a.title,
                Value = a.id.ToString()
            });

            mod.ProductUnits = productUnit.Select(a => new SelectListItem
            {
                Text = a.title,
                Value = a.id.ToString()
            });

            #region loadstores
            List<Store> stores = (from data in db.Stores
                                    where data.companyId == _companyid
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

            #region loadTax
            List<TaxCategory> tax = (from data in db.TaxCategories
                                    where data.companyid == _companyid
                                    select data).ToList();


            mod.TaxCategories = tax.Select(a => new SelectListItem
            {
                Text = a.title.ToString(),
                Value = a.id.ToString()+"-"+a.rate.ToString()
            });
            #endregion

            #region yesno
            var yesno = new List<string>();
            yesno.Add("Yes");
            yesno.Add("No");

            mod.YesNoOptions = yesno.Select(a => new SelectListItem
            {
                Text = a.ToString(),
                Value = a.ToString()
            });
            #endregion
            #endregion


            if (id == 0)
            {
                mod.SelectedProductUnitId = 1;
                mod.SelectedBarcodeTypeId = 1;
              
            }
            else
            {
                mod.productId = id;
            }
            Product product  = db.Products.SingleOrDefault(x => x.productid == id);
            if(product!=null)
            {
                Category category = db.Categories.SingleOrDefault(x => x.categoryId == product.categoryId);

                var taxRateObj = db.TaxCategories.SingleOrDefault(x => x.id == product.taxId.Value);
                if(category!=null && category.companyid==ClientSession.CompanyID)
                {
                    mod.SelectedBarcodeTypeId = product.barcodeTypeId.Value;
                    mod.SelectedCategoryId = product.categoryId.Value;
                    mod.ProductTitle = product.productTitle;
                    mod.Barcode = product.barcode;
                    mod.productId = product.productid;
                    mod.itemRate = product.itemSale.Value;
                    mod.itemCost = product.itemCost.Value;
                    mod.SelectedStoreId = category.storeid.Value; 
                    mod.isMisc = product.isMisc.Value;
                    mod.isCondimentItem = product.isCondimentItem.Value;
                    mod.taxId = product.taxId.Value+"-"+(taxRateObj!=null?taxRateObj.rate.ToString():"");
                    mod.productId = id;
                    mod.Active = product.active.Value;
                    mod.stocking = product.stocking.Value;
                    mod.minAlert = product.minAlert.Value;
                    mod.VisibleOnScreen = product.visibleOnScreen.Value;
                    mod.ProductCode = product.productCode;
                    mod.restrictMinSale = product.restrictMinimumSale.Value==true?"Yes":"No"; 
                }
                else
                {
                    throw new HttpException(404, "Product not found");
                }
            }
            else if (id > 0)
            {
                throw new HttpException(404, "Product not found");
            }
            return View(mod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditProduct(ProductModel model)
        {
            int _companyid = ClientSession.CompanyID;
            if(ModelState.IsValid)
            {
                Product product = null;
                
                if (model.productId > 0)
                {
                    product = db.Products.SingleOrDefault(x => x.productid == model.productId);
                    if (product == null)
                    {
                        throw new HttpException(404, "Product not found");
                    }
                    else
                    {
                        Category category = db.Categories.SingleOrDefault(x => x.categoryId == product.categoryId);
                        if (category != null && category.companyid == ClientSession.CompanyID)
                        {
                            product.productTitle = model.ProductTitle;
                            product.barcodeTypeId = model.SelectedBarcodeTypeId;
                            product.categoryId = model.SelectedCategoryId;
                            product.barcode = model.Barcode;
                            product.restrictMinimumSale = model.restrictMinSale.ToLower().Equals("no") ? false : true ;
                            product.productid = model.productId.Value;
                            product.itemSale = model.itemRate;
                            product.itemCost = model.itemCost;
                            product.isMisc = model.isMisc;
                            product.isCondimentItem = model.isCondimentItem;
                            product.active = model.Active;
                            product.stocking = model.stocking;
                            product.taxId = Convert.ToInt32(model.taxId.Split('-')[0]);
                            product.minAlert = model.minAlert;
                            product.visibleOnScreen = model.VisibleOnScreen;
                            product.productCode = model.ProductCode;
                            db.SaveChanges();
                           return RedirectToAction("ManageProducts", "Product");
                        }
                        else
                        {
                            throw new HttpException(404, "Product not found");
                        }
                    }
                }
                else
                {
                    //new product
                    product = new Product();
                    product.productTitle = model.ProductTitle;
                    product.barcodeTypeId = model.SelectedBarcodeTypeId;
                    product.categoryId = model.SelectedCategoryId;
                    product.taxId = Convert.ToInt32(model.taxId.Split('-')[0]);
                    product.barcode = model.Barcode;
                    product.productid = 0;
                    product.productUnit = model.SelectedProductUnitId;
                    product.itemSale = model.itemRate;
                    product.itemCost = model.itemCost;
                    product.isMisc = model.isMisc;
                    product.isCondimentItem = model.isCondimentItem;
                    product.active = model.Active;
                    product.stocking = model.stocking;
                    product.minAlert = model.minAlert;
                    product.createdBy = ClientSession.EmployeeID;
                    product.createdDate = DateTime.Now;
                    product.restrictMinimumSale = model.restrictMinSale.ToLower().Equals("yes")?true:false;
                    product.visibleOnScreen = model.VisibleOnScreen;
                    product.productCode = model.ProductCode;
                    db.Products.Add(product);
                    db.SaveChanges();

                    return RedirectToAction("ManageProducts", "Product");
                }
            }

            #region loadsettings
            ProductModel mod = new ProductModel();
            List<Category> categories = db.Categories.Where(x => x.companyid == _companyid).ToList();
            List<BarcodeType> barcodeType = db.BarcodeTypes.Where(x => x.active == true).ToList();
            List<ProductUnit> productUnit = db.ProductUnits.Where(x => x.active == true).ToList();
            List<TaxCategory> TaxUnit = db.TaxCategories.Where(x => x.active == true && x.companyid == _companyid).ToList();



            model.Categories = categories.Select(a => new SelectListItem
            {
                Text = a.categoryCode + "-" + a.categoryTitle,
                Value = a.categoryId.ToString()
            });

            model.BarcodeTypes = barcodeType.Select(a => new SelectListItem
            {
                Text = a.title,
                Value = a.id.ToString()
            });

            model.ProductUnits = productUnit.Select(a => new SelectListItem
            {
                Text = a.title,
                Value = a.id.ToString()
            });

            #region loadstores
            List<Store> stores = (from data in db.Stores
                                  where data.companyId == _companyid
                                  select data).ToList();

            Store objStores = new Store();
            objStores.storeName = "All Stores";
            objStores.storeID = 0;
            stores.Insert(0, objStores);
            model.Stores = stores.Select(a => new SelectListItem
            {
                Text = a.storeName,
                Value = a.storeID.ToString()
            });
            #endregion

            #region loadTax
            List<TaxCategory> tax = (from data in db.TaxCategories
                                     where data.companyid == _companyid
                                     select data).ToList();


            model.TaxCategories = tax.Select(a => new SelectListItem
            {
                Text = a.title.ToString(),
                Value = a.id.ToString() + "-" + a.rate.ToString()
            });
            #endregion

            #region yesno
            var yesno = new List<string>();
            yesno.Add("Yes");
            yesno.Add("No");

            model.YesNoOptions = yesno.Select(a => new SelectListItem
            {
                Text = a.ToString(),
                Value = a.ToString()
            });
            #endregion
            #endregion

            return View(model);
        }
    }
}
