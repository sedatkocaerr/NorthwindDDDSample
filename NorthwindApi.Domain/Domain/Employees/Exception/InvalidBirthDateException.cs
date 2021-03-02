using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Core.Domain.Excepiton;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.Employees.Exception
{
    public class InvalidBirthDateException : BaseException
    {
        public InvalidBirthDateException()
        {
            ExceptionType = ExcepitonTypes.InvalidBirthDateException;
        }
    }
}
