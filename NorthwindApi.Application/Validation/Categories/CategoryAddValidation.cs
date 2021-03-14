using NorthwindApi.Application.Commands.CategoriesCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Validation.Categories
{
    public class CategoryAddValidation : CategoryValidation<CategoryAddCommand>
    {
        public CategoryAddValidation()
        {
            ValidateCategoryName();
            ValidateCategoryDescription();
        }
    }
}
