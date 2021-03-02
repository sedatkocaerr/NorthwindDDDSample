using NorthwindApi.Domain.Domain.OrderDetails.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.OrderDetails
{
    public class OrderDetailsRules
    {
        public static void CheckOrderDetailUnitPrice(double unitPrice)
        {
            if(unitPrice<=0)
            {
                throw new InvalidUnitPriceException();
            }
        }

        public static void CheckOrderDetailQuantity(short quantity)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }
        }
    }
}
