using MediatR;
using NorthwindApi.Domain.Domain.Products;
using NorthwindApi.Domain.Validation.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Commands.ProductsCommands
{
    public class ProductUpdateCommand: ProductCommand, IRequest<Product>
    {

        public ProductUpdateCommand(Guid ıd, string productName, Guid supplierID, Guid categoryID, string quantityPerUnit, decimal unitPrice, double unitsInStock)
        {
            Id = ıd;
            ProductName = productName;
            SupplierID = supplierID;
            CategoryID = categoryID;
            QuantityPerUnit = quantityPerUnit;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
        }

        public override bool IsValid()
        {
            CommandResponse.ValidationResult = new ProductUpdateValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
