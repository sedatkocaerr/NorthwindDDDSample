using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Core.Domain.Excepiton;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.OrderDetails.Exceptions
{
    public class OrderDetailAlreadyExistException : BaseException
    {
        public OrderDetailAlreadyExistException()
        {
            ExceptionType = ExcepitonTypes.OrderDetailAlreadyExist;
        }
    }
}
