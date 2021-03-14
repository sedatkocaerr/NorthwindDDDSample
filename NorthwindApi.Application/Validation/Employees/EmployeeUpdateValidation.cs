using NorthwindApi.Application.Commands.EmployeesCommands;

namespace NorthwindApi.Application.Validation.Employees
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
