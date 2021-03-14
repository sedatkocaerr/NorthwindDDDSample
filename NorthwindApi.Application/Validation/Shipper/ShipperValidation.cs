using FluentValidation;
using NorthwindApi.Application.Commands.ShipperCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Shipper
{
    public class ShipperValidation<T>:AbstractValidator<T> where T : ShipperCommand
    {
        protected void ValidateId()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("{PropertyName} not be empty.");
        }

        protected void ValidateCompanyName()
        {
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("{PropertyName} not be empty.")
               .MaximumLength(40).WithMessage("{PropertyName} max size 40.")
               .MinimumLength(2).WithMessage("{PropertyName} min size 2.");
        }

        protected void ValidatePhone()
        {
            RuleFor(x => x.Phone).NotEmpty().WithMessage("{PropertyName} not be empty.")
               .MaximumLength(24).WithMessage("{PropertyName} max size 24.")
               .MinimumLength(9).WithMessage("{PropertyName} min size 9.");
        }
    }
}
