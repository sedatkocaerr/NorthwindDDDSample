using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Core.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Interfaces
{
    public interface IEmployeeAppService
    {
        Task<IEnumerable<EmployeeViewModel>> GetAll();
        Task<EmployeeViewModel> GetById(Guid id);

        Task<CommandResponse> AddEmployee(EmployeeViewModel employeeViewModel);
        Task<CommandResponse> UpdateEmployee(EmployeeViewModel employeeViewModel);
        Task<CommandResponse> Remove(Guid id);
    }
}
