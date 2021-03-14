using NorthwindApi.Application.Commands.CategoriesCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Validation.Categories
{
    public class CategoryUpdateValidation : CategoryValidation<CategoryUpdateCommand>
    {
        public CategoryUpdateValidation()
        {
            ValidateCategoryId();
            ValidateCategoryName();
            ValidateCategoryDescription();
        }
    }
}
