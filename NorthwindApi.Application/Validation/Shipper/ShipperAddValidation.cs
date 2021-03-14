using NorthwindApi.Application.Commands.ShipperCommands;

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
