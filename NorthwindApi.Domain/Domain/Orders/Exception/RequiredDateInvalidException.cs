using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Core.Domain.Excepiton;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.Orders.Exception
{
    public class RequiredDateInvalidException:BaseException
    {
        public RequiredDateInvalidException()
        {
            ExceptionType = ExcepitonTypes.InvalidRequiredDate;
        }
    }
}
