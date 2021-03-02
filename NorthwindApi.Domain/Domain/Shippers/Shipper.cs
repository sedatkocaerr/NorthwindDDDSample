using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Orders;
using NorthwindApi.Domain.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;
using NorthwindApi.Domain.Core.Domain;
namespace NorthwindApi.Domain.Domain.Shippers
{
    public class Shipper : Entity,IAggregateRoot
    {
        protected Shipper()
        {
            _orders = new List<Order>();
        }

        private List<Order> _orders;
        public string CompanyName { get; private set; }
        public string Phone { get; private set; }


        public virtual IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

       

        public Shipper (Guid id,string companyName,string phone)
        {
            TypeDataCheck.checkId(id);
            TypeDataCheck.IsNullOrEmpty(companyName);
            TypeDataCheck.IsNullOrEmpty(phone);

            SetId(id);
            CompanyName = companyName;
            Phone = phone;
        }


        public void  SetId(Guid id)
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

        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
