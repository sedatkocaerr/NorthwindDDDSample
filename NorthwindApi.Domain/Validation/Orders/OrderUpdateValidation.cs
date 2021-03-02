using NorthwindApi.Domain.Commands.Orders;
using System;
using System.Collections.Generic;
using System.Text;

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
