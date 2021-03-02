using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Employees;
using NorthwindApi.Domain.Events.EmployeesEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Commands.EmployeesCommands
{
    public class EmployeesCommandHandler : BaseCommandHandler,
        IRequestHandler<EmployeeAddCommand, ValidationResult>,
        IRequestHandler<EmployeeUpdateCommand, ValidationResult>,
        IRequestHandler<EmployeeRemoveCommand, ValidationResult>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IEventSourceRepository _eventStoreRepository;

        public EmployeesCommandHandler(IEmployeesRepository employeesRepository, IEventSourceRepository eventStoreRepository)
        {
            _employeesRepository = employeesRepository;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<ValidationResult> Handle(EmployeeAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var employee = new Employee(Guid.NewGuid(), request.FirstName, request.LastName, request.Title, request.BirthDate,
                request.HireDate, request.Address, request.City, request.PostalCode, request.Country);

            // Cretae Employee Event
            employee.Apply(new EmployeeAddEvent(employee.Id, employee.FirstName, employee.LastName,employee.Title, employee.BirthDate,
                employee.HireDate, employee.Address, employee.City));

            // save the Employee

            await _employeesRepository.Add(employee);

            // publish event

            await _eventStoreRepository.SaveAsync<Employee>(employee);

            return await Commit(_employeesRepository.UnitOfWork);
        
        }

        public async Task<ValidationResult> Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var checkEmployee = await _employeesRepository.ChechkEmployee(request.Id);
            if (!checkEmployee)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure(null, "This id not finded."));
                return request.ValidationResult;
            } 

            var employee = new Employee(request.Id, request.FirstName, request.LastName, request.Title, request.BirthDate,
                request.HireDate, request.Address, request.City, request.PostalCode, request.Country);

            // Cretae Employee Event
            employee.Apply(new EmployeeUpdateEvent(employee.Id, employee.FirstName, employee.LastName, employee.Title, employee.BirthDate,
                employee.HireDate, employee.Address, employee.City));

            // save the Employee

             _employeesRepository.Update(employee);

            // publish event

            await _eventStoreRepository.SaveAsync<Employee>(employee);

            return await Commit(_employeesRepository.UnitOfWork);
        }
        public async Task<ValidationResult> Handle(EmployeeRemoveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var employee = await _employeesRepository.FindById(request.Id);

            if (employee == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure(string.Empty, "employee not found "));
                return request.ValidationResult;
            }

            // Cretae Employee Event
            employee.Apply(new EmployeeRemoveEvent(employee.Id));

            // TODO Here will Add to Domain Event....

            _employeesRepository.Remove(employee);

            await _eventStoreRepository.SaveAsync<Employee>(employee);

            return await Commit(_employeesRepository.UnitOfWork);
        }

    }
}
