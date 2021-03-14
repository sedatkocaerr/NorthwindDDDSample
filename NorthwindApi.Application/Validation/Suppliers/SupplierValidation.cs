using FluentValidation;
using NorthwindApi.Application.Commands.SuppliersCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Suppliers
{
    public class SupplierValidation<T>:AbstractValidator<T> where T:SupplierCommand
    {
        protected void validateId()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id Not Empty");
        }
        protected void validateCompanyName()
        {
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("{Propertyname} not be empty.")
                .MaximumLength(40).WithMessage("{Propertyname} max lenght size 40");
        }

        protected void validateContactName()
        {
            RuleFor(x => x.ContactName).NotEmpty().WithMessage("{Propertyname} not be empty.")
                .MaximumLength(30).WithMessage("{Propertyname} max lenght size 30");
        }

        protected void validateContactTitle()
        {
            RuleFor(x => x.ContactTitle).NotEmpty().WithMessage("{Propertyname} not be empty.")
                .MaximumLength(30).WithMessage("{Propertyname} max lenght size 30");
        }

        protected void validateAdress()
        {
            RuleFor(x => x.Adress).NotEmpty().WithMessage("{Propertyname} not be empty.")
                .MaximumLength(60).WithMessage("{Propertyname} max lenght size 60");
        }

        protected void validateCity()
        {
            RuleFor(x => x.City).NotEmpty().WithMessage("{Propertyname} not be empty.")
                .MaximumLength(15).WithMessage("{Propertyname} max lenght size 15");
        }

        protected void validateCountry()
        {
            RuleFor(x => x.Country).NotEmpty().WithMessage("{Propertyname} not be empty.")
                .MaximumLength(15).WithMessage("{Propertyname} max lenght size 15");
        }

        protected void validatePhone()
        {
            RuleFor(x => x.Phone).NotEmpty().WithMessage("{Propertyname} not be empty.")
                .MinimumLength(9).WithMessage("{Propertyname} min lenght size 9")
                .MaximumLength(24).WithMessage("{Propertyname} max lenght size 24");
        }
    }

    
}
