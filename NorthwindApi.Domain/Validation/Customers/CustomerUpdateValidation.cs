using NorthwindApi.Domain.Commands.CustomersCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Customers
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
