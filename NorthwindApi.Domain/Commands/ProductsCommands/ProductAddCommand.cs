using MediatR;
using NorthwindApi.Domain.Domain.Products;
using NorthwindApi.Domain.Validation.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.ProductsCommands
{
    public class ProductAddCommand: ProductCommand,IRequest<Product>
    {
        public ProductAddCommand( string productName, Guid supplierID, Guid categoryID, 
            string quantityPerUnit, decimal unitPrice, double unitsInStock)
        {
            ProductName = productName;
            SupplierID = supplierID;
            CategoryID = categoryID;
            QuantityPerUnit = quantityPerUnit;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
        }

       

        public override bool IsValid()
        {
            ValidationResult = new ProductAddValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
