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
    
    public partial class Rate
    {
        public long rateID { get; set; }
        public string rateCode { get; set; }
        public string title { get; set; }
        public Nullable<decimal> amount { get; set; }
        public bool active { get; set; }
        public long storeID { get; set; }
        public long companyID { get; set; }
        public bool isPrepay { get; set; }
        public System.DateTime startTime { get; set; }
        public System.DateTime endTime { get; set; }
        public string color { get; set; }
        public Nullable<int> bufferTime { get; set; }
        public Nullable<int> alertInterval { get; set; }
        public Nullable<int> createdBy { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
        public Nullable<int> ratetype { get; set; }
    }
}
