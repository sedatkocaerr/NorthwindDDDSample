using NorthwindApi.Application.Commands.Orders;

namespace NorthwindApi.Domain.Validation.Orders
{
    public class OrderUpdateValidation:OrderValidation<OrderUpdateCommand>
    {
        public OrderUpdateValidation()
        {
            ValidateOrderId();
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
