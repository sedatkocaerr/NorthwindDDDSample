using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
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

        Task<ValidationResult> AddEmployee(EmployeeViewModel employeeViewModel);
        Task<ValidationResult> UpdateEmployee(EmployeeViewModel employeeViewModel);
        Task<ValidationResult> Remove(Guid id);
    }
}
