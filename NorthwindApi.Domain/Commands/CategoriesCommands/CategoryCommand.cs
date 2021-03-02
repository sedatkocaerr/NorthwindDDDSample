using MediatR;
using NorthwindApi.Domain.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.CategoriesCommands
{
    public class CategoryCommand:Command, IRequest<Category>
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Picture { get; protected set; }
    }
}
