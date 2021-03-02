using MediatR;
using NorthwindApi.Domain.Domain.Shippers;
using NorthwindApi.Domain.Validation.Shipper;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.ShipperCommands
{
    public class ShipperRemoveCommand : ShipperCommand, IRequest<Shipper>
    {
        public ShipperRemoveCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new ShipperRemoveValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
