using NorthwindApi.Domain.Commands.Orders;
using System;
using System.Collections.Generic;
using System.Text;

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
