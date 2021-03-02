using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Interfaces
{
    public interface ISupplierAppService
    {
        Task<IEnumerable<SupplierViewModel>> GetAll();
        Task<SupplierViewModel> GetById(Guid id);

        Task<ValidationResult> AddSupplier(SupplierViewModel supplierViewModel);
        Task<ValidationResult> UpdateSupplier(SupplierViewModel supplierViewModel);
        Task<ValidationResult> Remove(Guid id);
    }
}
