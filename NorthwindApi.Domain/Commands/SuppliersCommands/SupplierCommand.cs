using MediatR;
using NorthwindApi.Domain.Domain.Shippers;
using NorthwindApi.Domain.Domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.SuppliersCommands
{
    public class SupplierCommand:Command,IRequest<Supplier>
    {
        public Guid Id { get; protected set; }
        public string CompanyName { get; protected set; }
        public string ContactName { get; protected set; }
        public string ContactTitle { get; protected set; }
        public string Adress { get; protected set; }
        public string City { get; protected set; }
        public string Country { get; protected set; }
        public string Phone { get; protected set; }
    }
}
