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
    
    public partial class Shift
    {
        public long shfitID { get; set; }
        public string shiftCode { get; set; }
        public long storeID { get; set; }
        public System.DateTime startTime { get; set; }
        public System.DateTime endTime { get; set; }
        public bool active { get; set; }
        public Nullable<int> createdBy { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    }
}
