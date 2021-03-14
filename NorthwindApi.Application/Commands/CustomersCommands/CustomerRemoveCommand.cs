using MediatR;
using NorthwindApi.Application.Validation.Customers;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.Customers;
using System;

namespace NorthwindApi.Application.Commands.CustomersCommands
{
    public class CustomerRemoveCommand: CustomerCommand, IRequest<Customer>
    {
        public CustomerRemoveCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            CommandResponse.ValidationResult = new CustomerRemoveValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
