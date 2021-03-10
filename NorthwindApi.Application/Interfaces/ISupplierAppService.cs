using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Core.Command;
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

        Task<CommandResponse> AddSupplier(SupplierViewModel supplierViewModel);
        Task<CommandResponse> UpdateSupplier(SupplierViewModel supplierViewModel);
        Task<CommandResponse> Remove(Guid id);
    }
}
