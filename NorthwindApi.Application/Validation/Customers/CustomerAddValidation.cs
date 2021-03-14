using NorthwindApi.Application.Commands.CustomersCommands;

namespace NorthwindApi.Application.Validation.Customers
{
    public class CustomerAddValidation : CustomerValidation<CustomerAddCommand>
    {
        public CustomerAddValidation()
        {
            ValidateCompanyName();
            ValidateContactName();
            ValidateContactTitle();
            ValidateEmail();
            ValidateAddress();
            ValidateCity();
            ValidatePostalCode();
            ValidateCountry();
            ValidatePhone();
        }
    }
}
