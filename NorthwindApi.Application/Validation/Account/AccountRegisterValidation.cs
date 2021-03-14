using NorthwindApi.Application.Commands.AccountCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Validation.Account
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
