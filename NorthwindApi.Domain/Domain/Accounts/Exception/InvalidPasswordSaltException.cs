using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Core.Domain.Excepiton;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.Accounts.Exception
{
    public class InvalidPasswordSaltException:BaseException
    {
        public InvalidPasswordSaltException()
        {
            ExceptionType = ExcepitonTypes.InvalidPasswordSalt;
        }
    }
}
