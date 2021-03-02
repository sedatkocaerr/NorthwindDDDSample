using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;
using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Events.SupplierEvents;

namespace NorthwindApi.Domain.Domain.Suppliers
{
    public class Supplier : Entity, IAggregateRoot
    {
        protected Supplier()
        {
            _products = new List<Product>();
        }

        private List<Product> _products;
        public string CompanyName { get; private set; }
        public string ContactName { get; private set; }
        public string ContactTitle { get; private set; }
        public string Adress { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string Phone { get; private set; }

        public virtual IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        public  Supplier (Guid id,string companyName, string contactName, string contactTitle, string adress,
            string city,  string country, string phone)
        {
            TypeDataCheck.IsNullOrEmpty(companyName);
            TypeDataCheck.IsNullOrEmpty(contactName);
            TypeDataCheck.IsNullOrEmpty(contactTitle);
            TypeDataCheck.IsNullOrEmpty(adress);
            TypeDataCheck.IsNullOrEmpty(city);
            TypeDataCheck.IsNullOrEmpty(country);
            TypeDataCheck.IsNullOrEmpty(phone);

            SetId(id);
            CompanyName = companyName;
            ContactName = contactName;
            ContactTitle = contactTitle;
            Adress = adress;
            City = city;
            Country = country;
            Phone = phone;
        }

        public void SetId(Guid id) 
        {
            TypeDataCheck.checkId(id);
            Id = id;
        }

        public void AddProduct(Product product)
        {
            TypeDataCheck.IsNull(product);
            _products.Add(product);
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case SupplierAddEvent x: OnCreatedSupplier(x); break;
                case SupplierUpdateEvent x: OnUpdateSupplier(x); break;
                case SupplierRemoveEvent x: OnRemovedSupplier(x); break;
            }
        }

        private void OnRemovedSupplier(SupplierRemoveEvent @event)
        {
            Id = @event.Id;
        }

        private void OnUpdateSupplier(SupplierUpdateEvent @event)
        {
            Id = @event.Id;
            CompanyName = @event.CompanyName;
            ContactName = @event.ContactName;
            ContactTitle = @event.ContactTitle;
            Adress = @event.Adress;
            City = @event.City;
            Country = @event.Country;
            Phone = @event.Phone;
        }


        private void OnCreatedSupplier(SupplierAddEvent @event)
        {
            Id = @event.Id;
            CompanyName = @event.CompanyName;
            ContactName = @event.ContactName;
            ContactTitle = @event.ContactTitle;
            Adress = @event.Adress;
            City = @event.City;
            Country = @event.Country;
            Phone = @event.Phone;
        }

       
    }
}
