using NorthwindApi.Domain.Commands.ProductsCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Product
{
    public class ProductRemoveValidation:ProductValidation<ProductRemoveCommand>
    {
        public ProductRemoveValidation()
        {
            ValidateId();
        }
    }
}
