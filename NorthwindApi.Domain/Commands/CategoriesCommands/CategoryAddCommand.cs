using MediatR;
using NorthwindApi.Domain.Domain.Categories;
using NorthwindApi.Domain.Validation.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Domain.Commands.CategoriesCommands
{
    public class CategoryAddCommand: CategoryCommand
    {
        public CategoryAddCommand(string name, string description="", string picture = "")
        {
            Name = name;
            Description = description;
            Picture = picture;
        }
        public override bool IsValid()
        {
            ValidationResult = new CategoryAddValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
