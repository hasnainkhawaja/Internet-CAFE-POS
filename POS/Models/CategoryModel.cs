using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using POSEntity;
using System.Web.Mvc;
using POS.Helpers;

namespace POS.Models
{
    public class CategoryModel
    {

        public IEnumerable<SelectListItem> Stores { get; set; }
       
        [Display(Name = "Store")]
        [Required] 
        public Int64 SelectedStoreId { get; set; }


        public IEnumerable<SelectListItem> Categories { get; set; }
        [Display(Name = "Parent Category")]
        [Required] 
        public Int64 SelectedParentCategoryId { get; set; } 

        public Int64? categoryId { get; set; } 
        [Display(Name = "Category Tilte")]
        [Required]
        public String CategoryTilte { get; set; }


        [Display(Name = "Category Code")]
        [Required]
        [Remote("ValidateCategory", "Category",AdditionalFields="categoryId", HttpMethod = "POST", ErrorMessage = "Category code already exists.")]
        public String CategoryCode { get; set; }


        [Display(Name = "In Discount")]
        public bool appliesDiscount { get; set; }
         
        public Int64 parentId { get; set; }
        public Int64 storeid { get; set; }
        public int companyid { get; set; }

        [Display(Name = "Print Order")]
        [Required]
        [Range(0,100)]
        public int printOrder { get; set; } 

        [Display(Name = "Status")]
        public bool isActive { get; set; } 

    }





}