using NorthwindApi.Domain.Validation.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.Orders
{
    public class OrderUpdateCommand:OrderCommand
    {
        public OrderUpdateCommand(Guid orderId, Guid customerID, Guid employeeID, DateTime orderDate,
             DateTime requiredDate, DateTime shippedDate, string shipName, string shipAddress, string shipCity
             , string shipPostalCode, string shipCountry, Guid shipVia)
        {
            Id = orderId;
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
            ValidationResult = new OrderUpdateValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
