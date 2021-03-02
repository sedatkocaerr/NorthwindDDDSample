using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.SupplierEvents
{
    public class SupplierUpdateEvent:Event
    {
        public SupplierUpdateEvent(Guid Id, string companyName, string contactName, string contactTitle, string adress,
            string city, string country, string phone)
        {
            this.Id = Id;
            CompanyName = companyName;
            ContactName = contactName;
            ContactTitle = contactTitle;
            Adress = adress;
            City = city;
            Country = country;
            Phone = phone;
        }

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
