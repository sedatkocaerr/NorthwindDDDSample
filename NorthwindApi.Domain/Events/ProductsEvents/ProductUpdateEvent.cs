using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.ProductsEvents
{
    public class ProductUpdateEvent:Event,INotification
    {
        public ProductUpdateEvent(Guid Id, string productName, Guid supplierID, Guid categoryID,
            string quantityPerUnit, decimal unitPrice, double unitsInStock)
        {
            this.Id = Id;
            ProductName = productName;
            SupplierID = supplierID;
            CategoryID = categoryID;
            QuantityPerUnit = quantityPerUnit;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
        }

        public Guid Id { get; protected set; }
        public string ProductName { get; protected set; }
        public Guid SupplierID { get; protected set; }
        public Guid CategoryID { get; protected set; }
        public string QuantityPerUnit { get; protected set; }
        public decimal UnitPrice { get; protected set; }
        public double UnitsInStock { get; protected set; }
    }
}
