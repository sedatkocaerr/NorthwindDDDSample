using NorthwindApi.Domain.Validation.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.AccountCommands
{
    public class AccountRegisterCommand:AccountCommand
    {
        public AccountRegisterCommand(string name,string surName,string email,string password)
        {
           Name = name; 
           SurName = surName; 
           Email = email;
           Password = password;
        }
        public override bool IsValid()
        {
            ValidationResult  = new AccountRegisterValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
