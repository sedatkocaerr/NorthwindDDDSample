using MediatR;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Domain.Validation.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.CustomersCommands
{
    public class CustomerRemoveCommand: CustomerCommand, IRequest<Customer>
    {
        public CustomerRemoveCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new CustomerRemoveValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
