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
            ValidateOrderDate();
            ValidateRequiredDate();
            ValidateShippedDate();
            ValidateShipName();
            ValidateShipAddress();
            ValidateShipCity();
            ValidateShipPostalCode();
            ValidateShipCountry();

        }
    }
}
