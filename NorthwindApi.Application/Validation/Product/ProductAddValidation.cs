using NorthwindApi.Application.Commands.ProductsCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Product
{
    public class ProductAddValidation: ProductValidation<ProductAddCommand>
    {
        public ProductAddValidation()
        {
            ValidateProductName();
            ValidateSupplierID();
            ValidateCategoryID();
            ValidateQuantityPerUnit();
            ValidateUnitPrice();
            ValidateUnitsInStock();
        }
    }
}
