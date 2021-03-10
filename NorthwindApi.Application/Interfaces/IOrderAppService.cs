using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Core.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Interfaces
{
    public interface IOrderAppService
    {
        Task<IEnumerable<OrderViewModel>> GetAll();
        Task<OrderViewModel> GetById(Guid id);

        Task<CommandResponse> AddOrder(OrderViewModel OrderViewModel);
        Task<ValidationResult> AddOrderDetail(OrderDetailViewModel addOrderDetail);
        Task<CommandResponse> UpdateOrder(OrderViewModel OrderViewModel);
        Task<CommandResponse> Remove(Guid id);
    }
}
