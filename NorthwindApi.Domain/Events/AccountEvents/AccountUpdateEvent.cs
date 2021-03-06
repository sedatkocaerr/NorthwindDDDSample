using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.AccountEvents
{
    public class AccountUpdateEvent:Event
    {
        public AccountUpdateEvent(Guid id, string name, string surName, string eMail)
        {
            Id = id;
            Name = name;
            SurName = surName;
            EMail = eMail;
        }

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string SurName { get; protected set; }
        public string EMail { get; protected set; }
    }
}
