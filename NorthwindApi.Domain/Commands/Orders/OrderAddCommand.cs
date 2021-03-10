using MediatR;
using NorthwindApi.Domain.Validation.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.Orders
{
    public class OrderAddCommand:OrderCommand
    {
        public OrderAddCommand( Guid customerID, Guid employeeID, DateTime orderDate,
            DateTime requiredDate, DateTime shippedDate, string shipName, string shipAddress, string shipCity
            , string shipPostalCode, string shipCountry, Guid shipVia)
        {
            CustomerID = customerID;
            EmployeeID = employeeID;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipPostalCode = shipPostalCode;
            ShipCountry = shipCountry;
            ShipVia = shipVia;
        }
        public override bool IsValid()
        {
            CommandResponse.ValidationResult = new OrderAddValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }

    }
}
