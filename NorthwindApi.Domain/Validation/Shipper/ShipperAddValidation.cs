using NorthwindApi.Domain.Commands.ShipperCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Shipper
{
    public class ShipperAddValidation:ShipperValidation<ShipperAddCommand>
    {
        public ShipperAddValidation()
        {
           
            ValidateCompanyName();
            ValidatePhone();
        }
    }
}
