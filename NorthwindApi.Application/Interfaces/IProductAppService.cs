using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Core.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Interfaces
{
    public interface IProductAppService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(Guid id);

        Task<CommandResponse> AddProduct(ProductViewModel productViewModel);
        Task<CommandResponse> UpdateProduct(ProductViewModel productViewModel);
        Task<CommandResponse> Remove(Guid id);
    }
}
