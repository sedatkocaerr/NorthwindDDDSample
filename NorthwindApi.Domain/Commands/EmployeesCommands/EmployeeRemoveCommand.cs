using NorthwindApi.Domain.Validation.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.EmployeesCommands
{
    public class EmployeeRemoveCommand:EmployeesCommand
    {
        public EmployeeRemoveCommand(Guid employeeId)
        {
            Id = employeeId;
        }
        public override bool IsValid()
        {
            CommandResponse.ValidationResult = new EmployeeRemoveValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
