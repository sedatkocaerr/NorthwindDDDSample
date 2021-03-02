using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Core.Domain.Excepiton;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.Products.Exception
{
    public class InvalidProductPriceException: BaseException
    {
        public InvalidProductPriceException()
        {
            ExceptionType = ExcepitonTypes.InvalidRequiredDate;
        }
    }
}
