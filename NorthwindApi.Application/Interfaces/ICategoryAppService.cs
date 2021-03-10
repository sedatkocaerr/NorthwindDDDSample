using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Core.Command;
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

        Task<CommandResponse> AddCategory(CategoryViewModel categoryViewModel);
        Task<CommandResponse> UpdateCategory(CategoryViewModel categoryViewModel);
        Task<CommandResponse> Remove(Guid id);
    }
}
