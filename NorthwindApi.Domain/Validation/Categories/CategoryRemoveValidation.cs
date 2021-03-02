using NorthwindApi.Domain.Commands.CategoriesCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Categories
{
    public class CategoryRemoveValidation : CategoryValidation<CategoryRemoveCommand>
    {
        public CategoryRemoveValidation()
        {
            ValidateCategoryId();
        }
    }
}
