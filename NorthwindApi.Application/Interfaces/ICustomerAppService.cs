using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Interfaces
{
    public interface ICustomerAppService
    {
        Task<IEnumerable<CustomerViewModel>> GetAll();
        Task<CustomerViewModel> GetById(Guid id);

        Task<ValidationResult> AddCustomer(CustomerViewModel customerViewModel);
        Task<ValidationResult> UpdateCustomer(CustomerViewModel customerViewModel);
        Task<ValidationResult> Remove(Guid id);
    }
}
