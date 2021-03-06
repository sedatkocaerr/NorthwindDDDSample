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
        public DateTime RegisterDate { get; private set; }

        public Account(Guid id,string name, string surname, string email, DateTime registerDate)
        {
            SetId(id);
            SetName(name);
            SetSurname(surname);
            SetEmail(email);
        }

        public void SetId(Guid id)
        {
            TypeDataCheck.checkId(id);
            Id = id;
        }

        public void SetName(string name)
        {
            TypeDataCheck.IsNullOrEmpty(Name);
            Name = name;
        }

        public void SetSurname(string surname)
        {
            TypeDataCheck.IsNullOrEmpty(Name);
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

        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
