using MediatR;
using NorthwindApi.Domain.Domain.Shippers;
using NorthwindApi.Domain.Validation.Shipper;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.ShipperCommands
{
    public class ShipperAddCommand: ShipperCommand, IRequest<Shipper>
    {
        public ShipperAddCommand(string companyName, string phone)
        {
            CompanyName = companyName;
            Phone = phone;
        }
        public override bool IsValid()
        {
            ValidationResult  = new ShipperAddValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
