using FluentValidation;
using NorthwindApi.Domain.Commands.CustomersCommands;
using System;

namespace NorthwindApi.Domain.Validation.Customers
{
    public abstract class CustomerValidation<T>:AbstractValidator<T> where T : CustomerCommand
    {
        protected void ValidateCustomerID()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateCompanyName()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("{PropertyName} Not Be Empty")
                .MaximumLength(40)
                .WithMessage("{PropertyName} Must have a maximum length of 40");
        }

        protected void ValidateContactName()
        {
            RuleFor(x => x.ContactName)
                .NotEmpty()
                .WithMessage("{PropertyName} Not Be Empty")
                .MaximumLength(30)
                .WithMessage("{PropertyName} Must have a maximum length of 30");
        }

        protected void ValidateContactTitle()
        {
            RuleFor(x => x.ContactTitle)
                .NotEmpty()
                .WithMessage("{PropertyName} Not Be Empty")
                .MaximumLength(30)
                .WithMessage("{PropertyName} Must have a maximum length of 30");
        }

        protected void ValidateEmail()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} Not Be Empty")
                .MaximumLength(100)
                .WithMessage("{PropertyName} Must have a maximum length of 100")
                .MinimumLength(10)
                .WithMessage("{PropertyName} Must have a minimum length of 10"); ;
        }


        protected void ValidateAddress()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("{PropertyName} Not Be Empty")
                .MaximumLength(60)
                .WithMessage("{PropertyName} Must have a maximum length of 60")
                .MinimumLength(5)
                .WithMessage("{PropertyName} Must have a minimum length of 5");
        }

        protected void ValidateCity()
        {
            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("{PropertyName} Not Be Empty")
                .MaximumLength(15)
                .WithMessage("{PropertyName} Must have a maximum length of 15");
        }
        
        protected void ValidatePostalCode()
        {
            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .WithMessage("{PropertyName} Not Be Empty")
                .MaximumLength(10)
                .WithMessage("{PropertyName} Must have a maximum length of 10");
        }

        protected void ValidateCountry()
        {
            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("{PropertyName} Not Be Empty")
                .MaximumLength(15)
                .WithMessage("{PropertyName} Must have a maximum length of 15");
        }
        
        protected void ValidatePhone()
        {
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("{PropertyName} Not Be Empty")
                .MaximumLength(24)
                .WithMessage("{PropertyName} Must have a maximum length of 24")
                .MinimumLength(9)
                .WithMessage("{PropertyName} Must have a minimum length of 9");
        }
    }
}
