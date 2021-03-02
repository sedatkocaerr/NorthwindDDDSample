using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Core.Domain.Excepiton;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.OrderDetails.Exceptions
{
    public class InvalidUnitPriceException : BaseException
    {
        public InvalidUnitPriceException()
        {
            ExceptionType = ExcepitonTypes.InvalidUnitPrice;
        }
    }
}
