using NorthwindApi.Application.Commands.ProductsCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Product
{
    public class ProductUpdateValidation:ProductValidation<ProductUpdateCommand>
    {
        public ProductUpdateValidation()
        {
            ValidateId();
            ValidateProductName();
            ValidateSupplierID();
            ValidateCategoryID();
            ValidateQuantityPerUnit();
            ValidateUnitPrice();
            ValidateUnitsInStock();
        }
    }
}
