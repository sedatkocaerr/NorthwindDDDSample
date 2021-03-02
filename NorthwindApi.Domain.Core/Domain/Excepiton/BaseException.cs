using NorthwindApi.Domain.Core.Domain.Excepiton;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Core.Domain
{
    public abstract class BaseException : Exception
    {
        public ExcepitonTypes ExceptionType { get; set; }
    }
}
