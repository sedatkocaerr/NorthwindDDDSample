using MediatR;
using NorthwindApi.Domain.Domain.Products;
using NorthwindApi.Domain.Validation.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.ProductsCommands
{
    public class ProductRemoveCommand: ProductCommand, IRequest<Product>
    {
        public ProductRemoveCommand(Guid ıd)
        {
            Id = ıd;
        }

        public override bool IsValid()
        {
            ValidationResult = new ProductRemoveValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
