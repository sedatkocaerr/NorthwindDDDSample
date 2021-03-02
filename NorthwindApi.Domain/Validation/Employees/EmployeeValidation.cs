using FluentValidation;
using NorthwindApi.Domain.Commands.EmployeesCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Employees
{
    public abstract class EmployeeValidation<T>:AbstractValidator<T> where T:EmployeesCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

        protected void ValidateFirstName()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(10).WithMessage("{PropertyName} Must have a maximum length of  10.")
                .NotEmpty().WithMessage("{PropertyName} not be null.");
        }

        protected void ValidateLastName()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(20).WithMessage("{PropertyName} Must have a maximum length of 20")
                .NotEmpty().WithMessage("{PropertyName} not be null.");
        }

        protected void ValidateBirthdate()
        {
            RuleFor(user => user.BirthDate)
           .Must(p => !(p == DateTime.MinValue))
           .WithMessage("BirthDate not null");
        }

        protected void ValidateHireDate()
        {
            RuleFor(user => user.BirthDate)
           .Must(p => !(p == DateTime.MinValue))
           .WithMessage("BirthDate not null");
        }

        protected void ValidateTitle()
        {
            RuleFor(x => x.Title)
                .MaximumLength(30)
                .WithMessage("{PropertyName} Must have a maximum length of  30.");
        }

        protected void ValidateAddress()
        {
            RuleFor(x => x.Address)
                .MaximumLength(60)
                .WithMessage("{PropertyName} Must have a maximum length of  60")
                .MinimumLength(5)
                .WithMessage("{PropertyName} Must have a maximum length of  5");
        }

        protected void ValidateCity()
        {
            RuleFor(x => x.City)
                .MaximumLength(15)
                .WithMessage("{PropertyName} maximum lenght should be 15 lenght");
        }

        protected void ValidatePostalCode()
        {
            RuleFor(x => x.PostalCode)
                .MaximumLength(10)
                .WithMessage("{PropertyName} maximum lenght should be 10 lenght");
        }

        protected void ValidateCountry()
        {
            RuleFor(x => x.Country)
                .MaximumLength(15)
                .WithMessage("{PropertyName} maximum lenght should be 15 lenght");
        }

    }
}
