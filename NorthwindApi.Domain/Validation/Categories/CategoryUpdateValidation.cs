using NorthwindApi.Domain.Commands.CategoriesCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Categories
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
