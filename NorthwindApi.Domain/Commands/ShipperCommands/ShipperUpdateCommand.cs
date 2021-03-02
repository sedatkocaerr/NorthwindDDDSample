using MediatR;
using NorthwindApi.Domain.Domain.Shippers;
using NorthwindApi.Domain.Validation.Shipper;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.ShipperCommands
{
    public class ShipperUpdateCommand: ShipperCommand, IRequest<Shipper>
    {
        public ShipperUpdateCommand(Guid id, string companyName, string phone)
        {
            Id = id;
            CompanyName = companyName;
            Phone = phone;
        }

        public override bool IsValid()
        {
            ValidationResult = new ShipperUpdateValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
