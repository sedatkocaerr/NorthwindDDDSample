using NorthwindApi.Domain.Commands.ShipperCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Shipper
{
    public class ShipperUpdateValidation:ShipperValidation<ShipperUpdateCommand>
    {
        public ShipperUpdateValidation()
        {
            ValidateId();
            ValidateCompanyName();
            ValidatePhone();
        }
    }
}
