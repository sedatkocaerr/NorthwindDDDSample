using NorthwindApi.Application.Validation.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Commands.EmployeesCommands
{
    public class EmployeeUpdateCommand : EmployeesCommand
    {
        public EmployeeUpdateCommand(Guid employeeId,string firstName, string lastName, string title, DateTime birthDate,
            DateTime hireDate, string address, string city, string postalCode, string country)
        {
            Id = employeeId;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            HireDate = hireDate;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        public override bool IsValid()
        {
            CommandResponse.ValidationResult = new EmployeeUpdateValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
