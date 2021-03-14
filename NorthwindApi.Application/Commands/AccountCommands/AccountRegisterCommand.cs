
using System;
using System.Collections.Generic;
using System.Text;
using NorthwindApi.Domain.Command;
using NorthwindApi.Application.Validation.Account;

namespace NorthwindApi.Application.Commands.AccountCommands
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
            CommandResponse.ValidationResult = new AccountRegisterValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
