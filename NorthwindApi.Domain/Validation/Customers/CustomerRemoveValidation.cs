using NorthwindApi.Domain.Commands.CustomersCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Customers
{
    public class CustomerRemoveValidation:CustomerValidation<CustomerRemoveCommand>
    {
        public CustomerRemoveValidation()
        {
            ValidateCustomerID();
        }
    }
}
