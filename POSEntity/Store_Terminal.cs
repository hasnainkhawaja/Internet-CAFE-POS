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
    
    public partial class Store_Terminal
    {
        public int terminalID { get; set; }
        public string terminalCode { get; set; }
        public string title { get; set; }
        public Nullable<System.Guid> connectionCode { get; set; }
        public Nullable<int> floorid { get; set; }
        public string color { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<int> createdBy { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDated { get; set; }
    }
}
