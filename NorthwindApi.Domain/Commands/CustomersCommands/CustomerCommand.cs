using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.CustomersCommands
{
    public abstract class CustomerCommand : Command
    {
        public Guid Id { get; protected set; }
        public string CompanyName { get; protected set; }
        public string ContactName { get; protected set; }
        public string ContactTitle { get; protected set; }
        public string Email { get; protected set; }
        public string Address { get; protected set; }
        public string City { get; protected set; }
        public string PostalCode { get; protected set; }
        public string Country { get; protected set; }
        public string Phone { get; protected set; }
    }
}
