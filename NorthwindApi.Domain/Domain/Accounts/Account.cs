using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.Accounts
{
    public class Account : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public DateTime RegisterDate { get; private set; }

        public Account(Guid id,string name, string surname, string email, byte[] PasswordHash,
            byte[] PasswordSalt, DateTime registerDate)
        {
            SetId(id);
            SetName(name);
            SetSurname(surname);
            SetEmail(email);
            SetpasswordHash(PasswordHash);
            SetPasswordSalt(PasswordSalt);
        }

        public void SetId(Guid id)
        {
            TypeDataCheck.checkId(id);
            Id = id;
        }

        public void SetName(string name)
        {
            TypeDataCheck.IsNullOrEmpty(name);
            Name = name;
        }

        public void SetSurname(string surname)
        {
            TypeDataCheck.IsNullOrEmpty(surname);
            Surname = surname;
        }
        public void SetEmail(string email)
        {
            AccountRuleCheck.CheckEmail(email);
            Email = email;
        }

        public void SetRegisterDate(DateTime registerDate)
        {
            if (DateTime.MinValue==registerDate)
            {
                registerDate = DateTime.Now;
            }
            RegisterDate = registerDate;
        }
        public void SetpasswordHash(byte[] passwordHash)
        {
            AccountRuleCheck.CheckPasswordHash(passwordHash);
            PasswordHash = passwordHash;
        }
        public void SetPasswordSalt(byte[] passwordSalt)
        {
            AccountRuleCheck.CheckPasswordSalt(passwordSalt);
            PasswordSalt = passwordSalt;
        }

        protected override void When(object @event)
        {
            
        }
    }
}
