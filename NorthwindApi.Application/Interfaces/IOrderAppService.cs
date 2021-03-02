using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
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

        Task<ValidationResult> AddOrder(OrderViewModel OrderViewModel);
        Task<ValidationResult> AddOrderDetail(OrderDetailViewModel addOrderDetail);
        Task<ValidationResult> UpdateOrder(OrderViewModel OrderViewModel);
        Task<ValidationResult> Remove(Guid id);
    }
}
