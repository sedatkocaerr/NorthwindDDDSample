using FluentValidation;
using NorthwindApi.Application.Commands.Orders;
using System;

namespace NorthwindApi.Domain.Validation.Orders
{
    public class OrderValidation<T>:AbstractValidator<T> where T:OrderCommand
    {

        protected void ValidateOrderId()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

        protected void ValidateCustomerID()
        {
            RuleFor(x => x.CustomerID).NotEqual(Guid.Empty);
        }

        protected void ValidateEmployeeID()
        {
            RuleFor(x => x.EmployeeID).NotEqual(Guid.Empty);
        }

        protected void ValidateShipVia()
        {
            RuleFor(x => x.ShipVia).NotEqual(Guid.Empty);
        }

        protected void ValidateOrderDate()
        {
            RuleFor(user => user.OrderDate)
           .Must(p => !(p == DateTime.MinValue))
           .WithMessage("BirthDate not null");
        }

        protected void ValidateRequiredDate()
        {
            RuleFor(user => user.RequiredDate)
           .Must(p => !(p == DateTime.MinValue))
           .WithMessage("BirthDate not null");
        }

        protected void ValidateShippedDate()
        {
            RuleFor(user => user.ShippedDate)
           .Must(p => !(p == DateTime.MinValue))
           .WithMessage("BirthDate not null");
        }

        protected void ValidateShipName()
        {
            RuleFor(x => x.ShipName)
                .MaximumLength(40)
                .WithMessage("{PropertyName} Must have a maximum length of 40.")
                .NotEmpty()
                .WithMessage("{PropertyName} not be null");
        }

        protected void ValidateShipAddress()
        {
            RuleFor(x => x.ShipAddress)
               .MaximumLength(60)
               .WithMessage("{PropertyName} Must have a maximum length of 60.")
               .NotEmpty()
               .WithMessage("{PropertyName} not be null");
        }

        protected void ValidateShipCity()
        {
            RuleFor(x => x.ShipCity)
               .MaximumLength(15)
               .WithMessage("{PropertyName} Must have a maximum length of 15.")
               .NotEmpty()
               .WithMessage("{PropertyName} not be null");
        }


        protected void ValidateShipPostalCode()
        {
            RuleFor(x => x.ShipPostalCode)
             .MaximumLength(10)
             .WithMessage("{PropertyName} Must have a maximum length of 10.");
        }

        protected void ValidateShipCountry()
        {
            RuleFor(x => x.ShipCountry)
             .NotEmpty()
             .WithMessage("{PropertyName} not be null")
            .MaximumLength(15)
            .WithMessage("{PropertyName} Must have a maximum length of 15.");
        }
    }
}
