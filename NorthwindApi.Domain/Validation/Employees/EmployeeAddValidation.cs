using NorthwindApi.Domain.Commands.EmployeesCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Employees
{
    public class EmployeeAddValidation:EmployeeValidation<EmployeeAddCommand>
    {
        public EmployeeAddValidation()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateTitle();
            ValidateAddress();
            ValidateBirthdate();
            ValidateHireDate();
            ValidateCity();
            ValidatePostalCode();
            ValidateCountry();
        }
    }
}
