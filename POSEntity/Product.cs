//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POSEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public long productid { get; set; }
        public string barcode { get; set; }
        public Nullable<int> barcodeTypeId { get; set; }
        public string productCode { get; set; }
        public string productTitle { get; set; }
        public Nullable<int> productUnit { get; set; }
        public Nullable<long> categoryId { get; set; }
        public Nullable<bool> visibleOnScreen { get; set; }
        public Nullable<bool> isMisc { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<decimal> itemCost { get; set; }
        public Nullable<decimal> itemSale { get; set; }
        public Nullable<int> createdBy { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
        public Nullable<bool> isCondimentItem { get; set; }
        public Nullable<bool> stocking { get; set; }
        public Nullable<bool> restrictMinimumSale { get; set; }
        public Nullable<decimal> minAlert { get; set; }
        public Nullable<int> taxId { get; set; }
    }
}
