using MediatR;
using NorthwindApi.Domain.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;
using NorthwindApi.Domain.Command;
namespace NorthwindApi.Application.Commands.Orders
{
    public class OrderCommand: Command, IRequest<Customer>
    {
        public Guid Id { get; protected set; }

        public Guid CustomerID { get; protected set; }

        public Guid EmployeeID { get; protected set; }

        public DateTime OrderDate { get; protected set; }

        public DateTime RequiredDate { get; protected set; }

        public DateTime ShippedDate { get; protected set; }

        public string ShipName { get; protected set; }

        public string ShipAddress { get; protected set; }

        public string ShipCity { get; protected set; }

        public string ShipPostalCode { get; protected set; }

        public string ShipCountry { get; protected set; }

        public Guid ShipVia { get; protected set; }

    }
}
