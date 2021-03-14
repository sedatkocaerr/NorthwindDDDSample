using FluentValidation;
using NorthwindApi.Application.Commands.ProductsCommands;

namespace NorthwindApi.Domain.Validation.Product
{
    public class ProductValidation<T> : AbstractValidator<T> where T : ProductCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} not be empty.");
        }
        

        protected void ValidateProductName()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("{PropertyName} not be empty.")
                .MaximumLength(40).WithMessage("{PropertyName} max size 40.");
        }

        protected void ValidateSupplierID()
        {
            RuleFor(x => x.SupplierID).NotEmpty().WithMessage("{PropertyName} not be empty.");
        }
        
        protected void ValidateCategoryID()
        {
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("{PropertyName} not be empty.");
        }

        protected void ValidateQuantityPerUnit()
        {
            RuleFor(x => x.QuantityPerUnit).NotEmpty().WithMessage("{PropertyName} not be empty.")
                 .MaximumLength(20).WithMessage("{PropertyName} max lenght size 20.");
        }

        protected void ValidateUnitPrice()
        {
            RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("{PropertyName} not be empty.");
        }

        protected void ValidateUnitsInStock()
        {
            RuleFor(x => x.UnitsInStock).NotEmpty().WithMessage("{PropertyName} not be empty.");
        }

    }
}
