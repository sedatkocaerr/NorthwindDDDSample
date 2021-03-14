using NorthwindApi.Application.Commands.CustomersCommands;

namespace NorthwindApi.Application.Validation.Customers
{
    public class CustomerUpdateValidation:CustomerValidation<CustomerUpdateCommand>
    {
        public CustomerUpdateValidation()
        {
            ValidateCustomerID();
            ValidateCompanyName();
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
