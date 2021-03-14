using NorthwindApi.Application.Validation.Categories;
using System;

namespace NorthwindApi.Application.Commands.CategoriesCommands
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
