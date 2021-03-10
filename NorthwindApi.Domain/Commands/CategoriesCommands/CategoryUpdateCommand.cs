using NorthwindApi.Domain.Validation.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.CategoriesCommands
{
    public class CategoryUpdateCommand : CategoryCommand
    {
        public CategoryUpdateCommand(Guid id, string name, string description, string picture)
        {
            Id = id;
            Name = name;
            Description = description;
            Picture = picture;
        }
        public override bool IsValid()
        {
            CommandResponse.ValidationResult = new CategoryUpdateValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
