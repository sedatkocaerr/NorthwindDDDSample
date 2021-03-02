using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Events.CustomersEvents;

namespace NorthwindApi.Domain.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        private List<Order> _orders;


        protected Customer()
        {
            _orders = new List<Order>();
        }
        public string CompanyName { get; protected set; }
        public string ContactName { get; protected set; }
        public string ContactTitle { get; protected set; }
        public string Email { get; protected set; }
        public string Address { get; protected set; }
        public string City { get; protected set; }
        public string PostalCode { get; protected set; }
        public string Country { get; protected set; }
        public string Phone { get; protected set; }
        public virtual IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();


        public Customer(Guid id, string companyName, string contactName,
            string contactTitle, string email, string address, string city, string postalCode, string country, string phone)
        {
            TypeDataCheck.checkId(id);
            TypeDataCheck.IsNull(companyName);
            TypeDataCheck.IsNull(contactName);
            TypeDataCheck.IsNull(contactTitle);
            TypeDataCheck.IsNull(email);
            TypeDataCheck.IsNull(address);
            TypeDataCheck.IsNull(city);
            TypeDataCheck.IsNull(postalCode);
            TypeDataCheck.IsNull(country);
            TypeDataCheck.IsNull(phone);

            this.SetId(id);
            this.SetEmail(email);
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

        public void SetId(Guid id)
        {
            TypeDataCheck.checkId(id);
            Id = id;
        }

        public void AddOrder(Order order)
        {
            TypeDataCheck.IsNull(order);
            TypeDataCheck.checkId(order.Id);
            _orders.Add(order);
        }

        public void SetEmail(string email)
        {
            TypeDataCheck.IsNullOrEmpty(email);
            CustomerRuleCheck.CheckEmail(email);
            Email = email;
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case CustomerAddEvent x: OnCreatedCustomer(x); break;
                case CustomerUpdateEvent x: OnUpdateCustomer(x); break;
                case CustomerRemoveEvent x: OnRemovedCustomer(x); break;
            }
        }

        private void OnRemovedCustomer(CustomerRemoveEvent @event)
        {
            Id = @event.Id;
        }

        private void OnUpdateCustomer(CustomerUpdateEvent @event)
        {
            Id = @event.Id;
            CompanyName = @event.CompanyName;
            ContactName = @event.ContactName;
            ContactTitle = @event.ContactTitle;
            Email = @event.Email;
            Address = @event.Address;
            City = @event.City;
            PostalCode = @event.PostalCode;
            Country = @event.Country;
            Phone = @event.Phone;
        }

        private void OnCreatedCustomer(CustomerAddEvent @event)
        {
            Id = @event.Id;
            CompanyName = @event.CompanyName;
            ContactName = @event.ContactName;
            ContactTitle = @event.ContactTitle;
            Email = @event.Email;
            Address = @event.Address;
            City = @event.City;
            PostalCode = @event.PostalCode;
            Country = @event.Country;
            Phone = @event.Phone;
        }
    }
}
