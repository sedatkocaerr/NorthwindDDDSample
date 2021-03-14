using MediatR;
using NorthwindApi.Application.Validation.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Commands.CategoriesCommands
{
    public class CategoryRemoveCommand : CategoryCommand
    {
        public CategoryRemoveCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            CommandResponse.ValidationResult = new CategoryRemoveValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
