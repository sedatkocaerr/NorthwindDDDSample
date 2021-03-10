using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Core.Command;
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

        Task<CommandResponse> AddCustomer(CustomerViewModel customerViewModel);
        Task<CommandResponse> UpdateCustomer(CustomerViewModel customerViewModel);
        Task<CommandResponse> Remove(Guid id);
    }
}
