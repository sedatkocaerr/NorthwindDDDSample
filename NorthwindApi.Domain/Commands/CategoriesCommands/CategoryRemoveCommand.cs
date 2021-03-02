using MediatR;
using NorthwindApi.Domain.Domain.Categories;
using NorthwindApi.Domain.Validation.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.CategoriesCommands
{
    public class CategoryRemoveCommand : CategoryCommand
    {
        public CategoryRemoveCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new CategoryRemoveValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
