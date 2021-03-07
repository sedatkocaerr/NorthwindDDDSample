using NorthwindApi.Domain.Commands.AccountCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Account
{
    public class AccountRegisterValidation:AccountValidation<AccountRegisterCommand>
    {
        public AccountRegisterValidation()
        {
            ValidateName();
            ValidateSurname();
            ValidateEmail();
            ValidatePassword();
        }
    }
}
