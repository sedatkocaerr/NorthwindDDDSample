using NorthwindApi.Domain.Domain.Customers.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindApi.Domain.Domain.Accounts
{
    public class AccountRuleCheck
    {
        public static void CheckEmail(string email)
        {
            bool data = new EmailAddressAttribute().IsValid(email);
            if (!data)
            {
                throw new InvalidEmailException();
            }
        }
    }
}
