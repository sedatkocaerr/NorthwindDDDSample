using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.CustomersEvents
{
    public class CustomerUpdateEvent:Event
    {
        public CustomerUpdateEvent(Guid Id, string companyName, string contactName, string contactTitle,
            string email, string address, string city,  string postalCode, string country, string phone)
        {
            this.Id = Id;
            CompanyName = companyName;
            ContactName = contactName;
            ContactTitle = contactTitle;
            Email = email;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Country = country;
            Phone = phone;

        }

        public Guid Id { get; set; }
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
