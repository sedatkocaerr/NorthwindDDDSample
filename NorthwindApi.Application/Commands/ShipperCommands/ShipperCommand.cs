using MediatR;
using NorthwindApi.Domain.Domain.Shippers;
using System;
using System.Collections.Generic;
using System.Text;
using NorthwindApi.Domain.Command;
namespace NorthwindApi.Application.Commands.ShipperCommands
{
    public class ShipperCommand:Command, IRequest<Shipper>
    {
        public Guid Id { get; protected set; }
        public string CompanyName { get; protected set; }
        public string Phone { get; protected set; }
    }
}
