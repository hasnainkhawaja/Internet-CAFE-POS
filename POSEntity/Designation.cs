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
    
    public partial class Designation
    {
        public int designationId { get; set; }
        public string title { get; set; }
        public Nullable<int> companyId { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<int> createdBy { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<int> modifiedBy { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
    }
}