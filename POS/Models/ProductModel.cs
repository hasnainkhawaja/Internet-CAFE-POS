using POS.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc; 

namespace POS.Models
{
    public class ProductModel
    {
        public IEnumerable<SelectListItem> Stores { get; set; }

        [Display(Name = "Store")]
        [Required]
        public Int64 SelectedStoreId { get; set; }


        public IEnumerable<SelectListItem> Categories { get; set; }
        [Display(Name = "Category")]
        [Required]
        [OptionVerificationAttribute]
        public Int64 SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> TaxCategories { get; set; }
        public IEnumerable<SelectListItem> YesNoOptions { get; set; } 


        public IEnumerable<SelectListItem> ProductUnits { get; set; }
        [Display(Name = "Product unit")]
        [Required]
        [OptionVerificationAttribute]
        public Int32 SelectedProductUnitId { get; set; }

        public IEnumerable<SelectListItem> BarcodeTypes { get; set; }
        [Display(Name = "Barcode Type")]
        [Required]
        public Int32 SelectedBarcodeTypeId { get; set; } 


        public Int64? productId { get; set; }

        [Display(Name = "Tilte")]
        [Required]
        public String ProductTitle { get; set; }


        [Display(Name = "Barcode")]
        [Remote("ValidateProductBarcode", "Product",AdditionalFields="productId", HttpMethod = "POST", ErrorMessage = "Barcode already exists.")]
        public String Barcode { get; set; }
         

        [Display(Name = "Code")]
        [Required]
        [Remote("ValidateProductCode", "Product",AdditionalFields="productId", HttpMethod = "POST", ErrorMessage = "Product code already exists.")]
        public String ProductCode { get; set; }

        [Display(Name = "Show On Screen")]
        public bool VisibleOnScreen { set; get; }

        [Display(Name = "Misc Item")]
        public bool isMisc { set; get; }

        [Display(Name = "Status")]
        public bool Active { set; get; }
         

        [Display(Name = "Stocking")]
        public bool stocking { set; get; }

        [Display(Name = "Condiment Item")]
        public bool isCondimentItem { set; get; }

        [Display(Name = "Item Cost")]
        [Required]
        public decimal itemCost { set; get; }

        [Display(Name = "Item Rate")]
        [Required]
        public decimal itemRate { set; get; }
         

        [Display(Name = "Tax Category")]
        [Required]
        public string taxId { set; get; }

        [Display(Name = "Tax Category")]
        [Required]
        public int selectedtaxId { set; get; }  

        [Display(Name = "Restrict Min Sale")]
        [Required]
        public string restrictMinSale { set; get; }

        [Display(Name = "Minimum Alert")]
        [Required]
        public decimal minAlert { set; get; }
    }
}