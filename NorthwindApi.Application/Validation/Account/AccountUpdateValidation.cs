using NorthwindApi.Application.Commands.AccountCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Validation.Account
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
