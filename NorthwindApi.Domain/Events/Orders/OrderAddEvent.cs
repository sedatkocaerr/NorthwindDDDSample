using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.Orders
{
    public class OrderAddEvent:Event
    {
        public OrderAddEvent(Guid Id, Guid customerID, Guid employeeID,
            DateTime orderDate, DateTime requiredDate, DateTime shippedDate,
            string shipName, string shipAddress, string shipCity, string shipPostalCode, string shipCountry, Guid shipVia)
        {
            this.Id = Id;
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

        public Guid Id { get; private set; }

        public Guid CustomerID { get; private set; }

        public Guid EmployeeID { get; private set; }

        public DateTime OrderDate { get; private set; }

        public DateTime RequiredDate { get; private set; }

        public DateTime ShippedDate { get; private set; }

        public string ShipName { get; private set; }

        public string ShipAddress { get; private set; }

        public string ShipCity { get; private set; }

        public string ShipPostalCode { get; private set; }

        public string ShipCountry { get; private set; }

        public Guid ShipVia { get; private set; }
    }
}
