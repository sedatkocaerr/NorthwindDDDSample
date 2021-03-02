using NorthwindApi.Domain.Commands.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Orders
{
    public class OrderRemoveValidation:OrderValidation<OrderRemoveCommand>
    {
        public OrderRemoveValidation()
        {
            ValidateOrderId();
        }
    }
}
