using FluentValidation;
using NorthwindApi.Domain.Commands.CategoriesCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Categories
{
    public abstract class CategoryValidation<T> : AbstractValidator<T> where T : CategoryCommand
    {
        
        protected void ValidateCategoryId()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

        protected void ValidateCategoryName()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} Not Be Empty")
            .MaximumLength(15).WithMessage("{PropertyName} Max lenght 15");
        }

        protected void ValidateCategoryDescription()
        {
            RuleFor(x => x.Description).MaximumLength(150).WithMessage("{PropertyName} Max lenght 150");
        }

    }
}
