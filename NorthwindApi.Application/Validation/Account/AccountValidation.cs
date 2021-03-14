using FluentValidation;
using NorthwindApi.Application.Commands.AccountCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Validation.Account
{
    public class AccountValidation<T>:AbstractValidator<T> where T: AccountCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

        protected void ValidateName()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} Not Be Empty")
            .MaximumLength(50).WithMessage("{PropertyName} Max lenght 50");
        }

        protected void ValidateSurname()
        {
            RuleFor(x => x.SurName).NotEmpty().WithMessage("{PropertyName} Not Be Empty")
            .MaximumLength(50).WithMessage("{PropertyName} Max lenght 50");
        }

        protected void ValidateEmail()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} Not Be Empty")
            .MaximumLength(100).WithMessage("{PropertyName} Max lenght 100");
        }

        protected void ValidatePassword()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("{PropertyName} Not Be Empty")
            .MaximumLength(100).WithMessage("{PropertyName} Max lenght 100");
        }
    }
}
