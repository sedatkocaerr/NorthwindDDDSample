using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
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

        Task<ValidationResult> AddProduct(ProductViewModel productViewModel);
        Task<ValidationResult> UpdateProduct(ProductViewModel productViewModel);
        Task<ValidationResult> Remove(Guid id);
    }
}
