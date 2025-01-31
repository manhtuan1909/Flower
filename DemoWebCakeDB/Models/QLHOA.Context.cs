﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoWebCakeDB.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class QLHOAEntities : DbContext
    {
        public QLHOAEntities()
            : base("name=QLHOAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderPro> OrderPros { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
    
        [DbFunction("QLHOAEntities", "GetCustomerByUsername")]
        public virtual IQueryable<GetCustomerByUsername_Result> GetCustomerByUsername(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetCustomerByUsername_Result>("[QLHOAEntities].[GetCustomerByUsername](@Username)", usernameParameter);
        }
    
        public virtual int AddCategoryProcedure(Nullable<int> id, string iDCate, string nameCate)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var iDCateParameter = iDCate != null ?
                new ObjectParameter("IDCate", iDCate) :
                new ObjectParameter("IDCate", typeof(string));
    
            var nameCateParameter = nameCate != null ?
                new ObjectParameter("NameCate", nameCate) :
                new ObjectParameter("NameCate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddCategoryProcedure", idParameter, iDCateParameter, nameCateParameter);
        }
    
        public virtual int AddProductProcedure(Nullable<int> productID, string namePro, string decriptionPro, Nullable<int> categoryId, Nullable<decimal> price, string imagePro)
        {
            var productIDParameter = productID.HasValue ?
                new ObjectParameter("ProductID", productID) :
                new ObjectParameter("ProductID", typeof(int));
    
            var nameProParameter = namePro != null ?
                new ObjectParameter("NamePro", namePro) :
                new ObjectParameter("NamePro", typeof(string));
    
            var decriptionProParameter = decriptionPro != null ?
                new ObjectParameter("DecriptionPro", decriptionPro) :
                new ObjectParameter("DecriptionPro", typeof(string));
    
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(int));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("Price", price) :
                new ObjectParameter("Price", typeof(decimal));
    
            var imageProParameter = imagePro != null ?
                new ObjectParameter("ImagePro", imagePro) :
                new ObjectParameter("ImagePro", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddProductProcedure", productIDParameter, nameProParameter, decriptionProParameter, categoryIdParameter, priceParameter, imageProParameter);
        }
    
        public virtual ObjectResult<Nullable<double>> CalculateOrderTotalProcedure(Nullable<int> orderId)
        {
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("OrderId", orderId) :
                new ObjectParameter("OrderId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("CalculateOrderTotalProcedure", orderIdParameter);
        }
    
        public virtual int DeleteCategoryProcedure(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteCategoryProcedure", idParameter);
        }
    
        public virtual int DeleteProductProcedure(Nullable<int> productID)
        {
            var productIDParameter = productID.HasValue ?
                new ObjectParameter("ProductID", productID) :
                new ObjectParameter("ProductID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteProductProcedure", productIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetCategoryCountProcedure()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetCategoryCountProcedure");
        }
    
        public virtual ObjectResult<string> GetCategoryNameByIdProcedure(Nullable<int> categoryId)
        {
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetCategoryNameByIdProcedure", categoryIdParameter);
        }
    
        public virtual ObjectResult<GetCustomerByUsernameProcedure_Result> GetCustomerByUsernameProcedure(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCustomerByUsernameProcedure_Result>("GetCustomerByUsernameProcedure", usernameParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetOrderCountByCustomerIdProcedure(Nullable<int> customerId)
        {
            var customerIdParameter = customerId.HasValue ?
                new ObjectParameter("CustomerId", customerId) :
                new ObjectParameter("CustomerId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetOrderCountByCustomerIdProcedure", customerIdParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetProductCountByCategoryProcedure(Nullable<int> categoryId)
        {
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetProductCountByCategoryProcedure", categoryIdParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> GetProductPriceByIdProcedure(Nullable<int> productId)
        {
            var productIdParameter = productId.HasValue ?
                new ObjectParameter("ProductId", productId) :
                new ObjectParameter("ProductId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("GetProductPriceByIdProcedure", productIdParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> GetVoucherValueByProductIdProcedure(Nullable<int> productId)
        {
            var productIdParameter = productId.HasValue ?
                new ObjectParameter("ProductId", productId) :
                new ObjectParameter("ProductId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("GetVoucherValueByProductIdProcedure", productIdParameter);
        }
    
        public virtual int UpdateCategoryProcedure(Nullable<int> id, string iDCate, string nameCate)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var iDCateParameter = iDCate != null ?
                new ObjectParameter("IDCate", iDCate) :
                new ObjectParameter("IDCate", typeof(string));
    
            var nameCateParameter = nameCate != null ?
                new ObjectParameter("NameCate", nameCate) :
                new ObjectParameter("NameCate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateCategoryProcedure", idParameter, iDCateParameter, nameCateParameter);
        }
    
        public virtual int UpdateProductProcedure(Nullable<int> productID, string namePro, string decriptionPro, Nullable<int> categoryId, Nullable<decimal> price, string imagePro)
        {
            var productIDParameter = productID.HasValue ?
                new ObjectParameter("ProductID", productID) :
                new ObjectParameter("ProductID", typeof(int));
    
            var nameProParameter = namePro != null ?
                new ObjectParameter("NamePro", namePro) :
                new ObjectParameter("NamePro", typeof(string));
    
            var decriptionProParameter = decriptionPro != null ?
                new ObjectParameter("DecriptionPro", decriptionPro) :
                new ObjectParameter("DecriptionPro", typeof(string));
    
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(int));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("Price", price) :
                new ObjectParameter("Price", typeof(decimal));
    
            var imageProParameter = imagePro != null ?
                new ObjectParameter("ImagePro", imagePro) :
                new ObjectParameter("ImagePro", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateProductProcedure", productIDParameter, nameProParameter, decriptionProParameter, categoryIdParameter, priceParameter, imageProParameter);
        }
    }
}
