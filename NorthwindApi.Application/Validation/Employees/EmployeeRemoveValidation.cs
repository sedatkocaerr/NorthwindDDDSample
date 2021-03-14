using NorthwindApi.Application.Commands.EmployeesCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Validation.Employees
{
    public class EmployeeRemoveValidation:EmployeeValidation<EmployeeRemoveCommand>
    {
        public EmployeeRemoveValidation()
        {
            ValidateId();
        }
    }
}
