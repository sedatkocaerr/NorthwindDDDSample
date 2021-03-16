using NorthwindApi.Application.Commands.Orders;

namespace NorthwindApi.Domain.Validation.Orders
{
    public class OrderAddValidation: OrderValidation<OrderAddCommand>
    {
        public OrderAddValidation()
        {
            ValidateCustomerID();
            ValidateEmployeeID();
            ValidateShipVia();
            ValidateRequiredDate();
            ValidateShipName();
            ValidateShipAddress();
            ValidateShipCity();
            ValidateShipPostalCode();
            ValidateShipCountry();

        }
    }
}
