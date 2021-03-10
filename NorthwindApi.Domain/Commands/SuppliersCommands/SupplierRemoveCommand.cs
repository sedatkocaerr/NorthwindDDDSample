using MediatR;
using NorthwindApi.Domain.Domain.Suppliers;
using NorthwindApi.Domain.Validation.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.SuppliersCommands
{
    public class SupplierRemoveCommand : SupplierCommand, IRequest<Supplier>
    {
        public SupplierRemoveCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            CommandResponse.ValidationResult = new SupplierRemoveValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
