using NorthwindApi.Domain.Commands.EmployeesCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Employees
{
    public class EmployeeUpdateValidation:EmployeeValidation<EmployeeUpdateCommand>
    {
        public EmployeeUpdateValidation()
        {
            ValidateId();
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
