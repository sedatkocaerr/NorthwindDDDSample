using NorthwindApi.Domain.Commands.AccountCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Account
{
    public class AccountUpdateValidation:AccountValidation<AccountUpdateCommand>
    {
        public AccountUpdateValidation()
        {
            ValidateId();
            ValidateName();
            ValidateSurname();
            ValidateEmail();
        }
    }
}
