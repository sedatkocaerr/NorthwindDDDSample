using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Interfaces
{
    public interface ICategoryAppService
    {
        Task<IEnumerable<CategoryViewModel>> GetAll();
        Task<CategoryViewModel> GetById(Guid id);

        Task<ValidationResult> AddCategory(CategoryViewModel categoryViewModel);
        Task<ValidationResult> UpdateCategory(CategoryViewModel categoryViewModel);
        Task<ValidationResult> Remove(Guid id);
    }
}
