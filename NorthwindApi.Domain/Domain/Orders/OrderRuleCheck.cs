using NorthwindApi.Domain.Domain.Orders.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.Orders
{
    public class OrderRuleCheck
    {
        public static void CheckRequiredDate(DateTime RequiredDate)
        {
            var hours = (RequiredDate - DateTime.Now).TotalHours;
            if (hours <= 12)
            {
                throw new RequiredDateInvalidException();
            }
        }
    }
}
