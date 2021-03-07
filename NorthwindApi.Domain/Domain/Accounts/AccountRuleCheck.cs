using NorthwindApi.Domain.Domain.Accounts.Exception;
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

        public static void CheckPasswordHash(byte[] passwordHash)
        {
            if (passwordHash == null && passwordHash.Length == 0)
            {
                throw new InvalidPasswordHashException();
            }
        }

        public static void CheckPasswordSalt(byte[] passwordSalt)
        {
            if (passwordSalt == null && passwordSalt.Length == 0)
            {
                throw new InvalidPasswordSaltException();
            }
        }
    }
}
