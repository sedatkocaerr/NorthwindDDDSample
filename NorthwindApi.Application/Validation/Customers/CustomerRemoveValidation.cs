using NorthwindApi.Application.Commands.CustomersCommands;

namespace NorthwindApi.Application.Validation.Customers
{
    public class CustomerRemoveValidation:CustomerValidation<CustomerRemoveCommand>
    {
        public CustomerRemoveValidation()
        {
            ValidateCustomerID();
        }
    }
}
