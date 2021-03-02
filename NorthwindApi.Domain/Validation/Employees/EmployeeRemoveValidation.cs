using NorthwindApi.Domain.Commands.EmployeesCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Employees
{
    public class EmployeeRemoveValidation:EmployeeValidation<EmployeeRemoveCommand>
    {
        public EmployeeRemoveValidation()
        {
            ValidateId();
        }
    }
}
