using FluentValidation;
using MediatR;
using NorthwindApi.Application.Validation.Categories;
using NorthwindApi.Domain.Core.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Application.Commands.CategoriesCommands
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
            CommandResponse.ValidationResult = new CategoryAddValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
