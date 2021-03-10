using NorthwindApi.Domain.Validation.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.AccountCommands
{
    public class AccountUpdateCommand : AccountCommand
    {
        public AccountUpdateCommand(Guid id,string name, string surName, string email)
        {
            Id = id;
            Name = name;
            SurName = surName;
            Email = email;
        }
        public override bool IsValid()
        {
            CommandResponse.ValidationResult = new AccountUpdateValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
