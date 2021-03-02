using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Categories;
using NorthwindApi.Domain.Domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;
using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Domain.OrderDetails;
using NorthwindApi.Domain.Events.ProductsEvents;

namespace NorthwindApi.Domain.Domain.Products
{
    public class Product :Entity, IAggregateRoot
    {
        protected Product()
        {

        }
        public string ProductName { get; private set; }
        public Guid SupplierID { get; private set; }
        public Guid CategoryID { get; private set; }
        public string QuantityPerUnit { get; private set; }
        public decimal UnitPrice { get; private set; }
        public double UnitsInStock { get; private set; }

        public Category Category { get; private set; }
        public Supplier Supplier { get; private set; }
        public List<OrderDetail> OrderDetails { get; private set; }

        public Product (Guid Id,Category category , Supplier supplier , string productName,
             string quantityPerUnit, decimal unitPrice, double unitsInStock)
        {
            // Model Control 
            TypeDataCheck.IsNull(category);
            TypeDataCheck.IsNull(supplier);

            TypeDataCheck.IsNullOrEmpty(productName);
            TypeDataCheck.IsNullOrEmpty(quantityPerUnit);

            SetId(Id);
            ProductName = productName;
            QuantityPerUnit = quantityPerUnit;
            SetCategoryId(category.Id);
            SetSupplierId(supplier.Id);
            SetUnitPrice(unitPrice);
            SetUnitsInStock(unitsInStock);
        }

        public void SetId(Guid id)
        {
            TypeDataCheck.checkId(id);
            Id = id;
        }

        public void SetCategoryId(Guid id)
        {
            TypeDataCheck.checkId(id);
            CategoryID = id;
        }
        public void SetSupplierId(Guid id)
        {
            TypeDataCheck.checkId(id);
            SupplierID = id;
        }

        public void SetUnitPrice (decimal unitPrice)
        {
            ProductRules.CheckUnitPrice(unitPrice);
            UnitPrice = unitPrice;
        }

        public void SetUnitsInStock(double unitsInStock)
        {
            ProductRules.CheckUnitsInStock(unitsInStock);
            UnitsInStock = unitsInStock;
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case ProductAddEvent x: OnCreatedProduct(x); break;
                case ProductUpdateEvent x: OnUpdateProduct(x); break;
                case ProductRemoveEvent x:OnRemovedProduct(x); break;
            }
        }

        private void OnRemovedProduct(ProductRemoveEvent @event)
        {
            Id = @event.Id;
        }

        private void OnUpdateProduct(ProductUpdateEvent @event)
        {
            Id = @event.Id;
            ProductName = @event.ProductName;
            SupplierID = @event.SupplierID;
            CategoryID = @event.CategoryID;
            QuantityPerUnit = @event.QuantityPerUnit;
            UnitPrice = @event.UnitPrice;
            UnitsInStock = @event.UnitsInStock;

        }


        private void OnCreatedProduct(ProductAddEvent @event)
        {
            Id = @event.Id;
            ProductName = @event.ProductName;
            SupplierID = @event.SupplierID;
            CategoryID = @event.CategoryID;
            QuantityPerUnit = @event.QuantityPerUnit;
            UnitPrice = @event.UnitPrice;
            UnitsInStock = @event.UnitsInStock;
        }
    }
}
