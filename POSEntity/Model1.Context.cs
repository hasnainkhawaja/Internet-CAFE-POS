﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class onlineinternetposEntities : DbContext
    {
        public onlineinternetposEntities()
            : base("name=onlineinternetposEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employee_encryptedpass> Employee_encryptedpass { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<EmployeeInfomation> EmployeeInfomations { get; set; }
        public virtual DbSet<Menu_List> Menu_List { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Floor> Floors { get; set; }
        public virtual DbSet<RateType> RateTypes { get; set; }
        public virtual DbSet<Store_Terminal> Store_Terminal { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<BarcodeType> BarcodeTypes { get; set; }
        public virtual DbSet<ProductUnit> ProductUnits { get; set; }
        public virtual DbSet<TaxCategory> TaxCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    
        public virtual ObjectResult<EmployeeInfomation_GetAll_Result> EmployeeInfomation_GetAll(Nullable<int> companyID)
        {
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeInfomation_GetAll_Result>("EmployeeInfomation_GetAll", companyIDParameter);
        }
    
        public virtual ObjectResult<EmployeeInfomation_GetByID_Result> EmployeeInfomation_GetByID(Nullable<int> employeeID)
        {
            var employeeIDParameter = employeeID.HasValue ?
                new ObjectParameter("employeeID", employeeID) :
                new ObjectParameter("employeeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeInfomation_GetByID_Result>("EmployeeInfomation_GetByID", employeeIDParameter);
        }
    
        public virtual ObjectResult<Store_SelectByCompanyID_Result> Store_SelectByCompanyID(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("companyId", companyId) :
                new ObjectParameter("companyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Store_SelectByCompanyID_Result>("Store_SelectByCompanyID", companyIdParameter);
        }
    
        public virtual ObjectResult<Store_SelectByStoreID_Result> Store_SelectByStoreID(Nullable<int> storeID, Nullable<int> companyId)
        {
            var storeIDParameter = storeID.HasValue ?
                new ObjectParameter("storeID", storeID) :
                new ObjectParameter("storeID", typeof(int));
    
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("companyId", companyId) :
                new ObjectParameter("companyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Store_SelectByStoreID_Result>("Store_SelectByStoreID", storeIDParameter, companyIdParameter);
        }
    
        public virtual ObjectResult<Designations_SelectAll_Result> Designations_SelectAll(Nullable<int> comapnyID, string active)
        {
            var comapnyIDParameter = comapnyID.HasValue ?
                new ObjectParameter("comapnyID", comapnyID) :
                new ObjectParameter("comapnyID", typeof(int));
    
            var activeParameter = active != null ?
                new ObjectParameter("active", active) :
                new ObjectParameter("active", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Designations_SelectAll_Result>("Designations_SelectAll", comapnyIDParameter, activeParameter);
        }
    
        public virtual ObjectResult<Rate_SelectByCompanyID_Result> Rate_SelectByCompanyID(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("companyId", companyId) :
                new ObjectParameter("companyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Rate_SelectByCompanyID_Result>("Rate_SelectByCompanyID", companyIdParameter);
        }
    
        public virtual ObjectResult<Rate_SelectByRateID_Result> Rate_SelectByRateID(Nullable<int> rateID)
        {
            var rateIDParameter = rateID.HasValue ?
                new ObjectParameter("rateID", rateID) :
                new ObjectParameter("rateID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Rate_SelectByRateID_Result>("Rate_SelectByRateID", rateIDParameter);
        }
    
        public virtual int Rate_Delete(Nullable<int> rateID)
        {
            var rateIDParameter = rateID.HasValue ?
                new ObjectParameter("rateID", rateID) :
                new ObjectParameter("rateID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Rate_Delete", rateIDParameter);
        }
    
        public virtual ObjectResult<Shfit_SelectByCompanyID_Result> Shfit_SelectByCompanyID(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("companyId", companyId) :
                new ObjectParameter("companyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Shfit_SelectByCompanyID_Result>("Shfit_SelectByCompanyID", companyIdParameter);
        }
    
        public virtual ObjectResult<Shfit_SelectByShiftID_Result> Shfit_SelectByShiftID(Nullable<int> shiftID)
        {
            var shiftIDParameter = shiftID.HasValue ?
                new ObjectParameter("shiftID", shiftID) :
                new ObjectParameter("shiftID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Shfit_SelectByShiftID_Result>("Shfit_SelectByShiftID", shiftIDParameter);
        }
    
        public virtual ObjectResult<Store_Terminal_ByCompanyID_Result> Store_Terminal_ByCompanyID(Nullable<int> companyID)
        {
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Store_Terminal_ByCompanyID_Result>("Store_Terminal_ByCompanyID", companyIDParameter);
        }
    
        public virtual ObjectResult<ValidateStoreTerminal_Result> ValidateStoreTerminal(Nullable<int> id, string title, Nullable<int> companyId)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var titleParameter = title != null ?
                new ObjectParameter("title", title) :
                new ObjectParameter("title", typeof(string));
    
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("companyId", companyId) :
                new ObjectParameter("companyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ValidateStoreTerminal_Result>("ValidateStoreTerminal", idParameter, titleParameter, companyIdParameter);
        }
    
        public virtual ObjectResult<ValidateUser_Result> ValidateUser(string userName, string password, Nullable<int> companyid)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var companyidParameter = companyid.HasValue ?
                new ObjectParameter("companyid", companyid) :
                new ObjectParameter("companyid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ValidateUser_Result>("ValidateUser", userNameParameter, passwordParameter, companyidParameter);
        }
    
        public virtual ObjectResult<SelectFloors_Result> SelectFloors(Nullable<int> companyid, Nullable<long> storeid)
        {
            var companyidParameter = companyid.HasValue ?
                new ObjectParameter("companyid", companyid) :
                new ObjectParameter("companyid", typeof(int));
    
            var storeidParameter = storeid.HasValue ?
                new ObjectParameter("storeid", storeid) :
                new ObjectParameter("storeid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SelectFloors_Result>("SelectFloors", companyidParameter, storeidParameter);
        }
    
        public virtual ObjectResult<Category_SelectByCompanyID_Result> Category_SelectByCompanyID(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("companyId", companyId) :
                new ObjectParameter("companyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Category_SelectByCompanyID_Result>("Category_SelectByCompanyID", companyIdParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> ValidateProductBarcode(Nullable<int> companyid, string barcode, Nullable<long> productId)
        {
            var companyidParameter = companyid.HasValue ?
                new ObjectParameter("companyid", companyid) :
                new ObjectParameter("companyid", typeof(int));
    
            var barcodeParameter = barcode != null ?
                new ObjectParameter("barcode", barcode) :
                new ObjectParameter("barcode", typeof(string));
    
            var productIdParameter = productId.HasValue ?
                new ObjectParameter("productId", productId) :
                new ObjectParameter("productId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ValidateProductBarcode", companyidParameter, barcodeParameter, productIdParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> ValidateProductCode(Nullable<int> companyid, string barcode, Nullable<long> productId)
        {
            var companyidParameter = companyid.HasValue ?
                new ObjectParameter("companyid", companyid) :
                new ObjectParameter("companyid", typeof(int));
    
            var barcodeParameter = barcode != null ?
                new ObjectParameter("barcode", barcode) :
                new ObjectParameter("barcode", typeof(string));
    
            var productIdParameter = productId.HasValue ?
                new ObjectParameter("productId", productId) :
                new ObjectParameter("productId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ValidateProductCode", companyidParameter, barcodeParameter, productIdParameter);
        }
    
        public virtual ObjectResult<Product_SelectByCompanyAndStore_Result> Product_SelectByCompanyAndStore(Nullable<int> companyId, Nullable<long> storeId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("companyId", companyId) :
                new ObjectParameter("companyId", typeof(int));
    
            var storeIdParameter = storeId.HasValue ?
                new ObjectParameter("storeId", storeId) :
                new ObjectParameter("storeId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Product_SelectByCompanyAndStore_Result>("Product_SelectByCompanyAndStore", companyIdParameter, storeIdParameter);
        }
    
        public virtual ObjectResult<TaxCategory_SelectByCompanyID_Result> TaxCategory_SelectByCompanyID(Nullable<int> companyId)
        {
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("companyId", companyId) :
                new ObjectParameter("companyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TaxCategory_SelectByCompanyID_Result>("TaxCategory_SelectByCompanyID", companyIdParameter);
        }
    }
}
