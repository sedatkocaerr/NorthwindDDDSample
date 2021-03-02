using NorthwindApi.Domain.Commands.CustomersCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Customers
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
