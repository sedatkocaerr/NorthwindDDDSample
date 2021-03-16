using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Domain.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Text;
using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Domain.OrderDetails;
using System.Linq;
using NorthwindApi.Domain.Domain.OrderDetails.Exceptions;
using NorthwindApi.Domain.Domain.Products;
using NorthwindApi.Domain.Domain.Shippers;
using NorthwindApi.Domain.Events.Orders;

namespace NorthwindApi.Domain.Domain.Orders
{
    public class Order:Entity, IAggregateRoot
    {
        protected Order()
        {
            _orderDetails = new List<OrderDetail>();
        }

        private readonly List<OrderDetail> _orderDetails;


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

        public Customer Customer { get; private set; }
        public Employee Employee { get; private set; }
        public Shipper Shipper { get; private set; }

        public virtual IReadOnlyCollection<OrderDetail> OrderDetail => _orderDetails.AsReadOnly();



        public  Order (Guid id,Customer customer , Employee employee, DateTime RequiredDate, string shipName, string shipAddress,
            string shipCity,  string shipPostalCode, string shipCountry, Guid shipVia)
        {
            if (_orderDetails==null)
            {
                _orderDetails = new List<OrderDetail>();
            }
            TypeDataCheck.IsNull(customer);
            TypeDataCheck.checkId(customer.Id);

            TypeDataCheck.IsNull(employee);
            TypeDataCheck.checkId(employee.Id);

            TypeDataCheck.checkId(shipVia);

            TypeDataCheck.IsNull(shipName);
            TypeDataCheck.IsNull(shipAddress);
            TypeDataCheck.IsNull(shipCity);
            TypeDataCheck.IsNull(shipCountry);

            SetId(id);
            CustomerID = customer.Id;
            EmployeeID = employee.Id;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipPostalCode = shipPostalCode;
            ShipCountry = shipCountry;
            ShipVia = shipVia;
            SetOrderDate();
            setShippedDate();
            setRequiredDate(RequiredDate);
        }

        public void AddOrderDetail(OrderDetail orderdetail)
        {
            TypeDataCheck.IsNull(orderdetail);
            TypeDataCheck.checkId(orderdetail.OrderID);
            TypeDataCheck.checkId(orderdetail.ProductId);

            var exitsOrderDetail =  _orderDetails.Where(x => x.ProductId == orderdetail.ProductId && x.OrderID == orderdetail.OrderID).SingleOrDefault();
            if(exitsOrderDetail!=null)
            {
                throw new OrderDetailAlreadyExistException();
            }
            
            _orderDetails.Add(orderdetail);
        }

        // Sipariş oluşma Tarihi Entity Oluştuğu andaki Zaman Alınır.
        public  void SetOrderDate()
        {
            OrderDate =DateTime.Now;
        }

        // Alınan Siparişler İş kurallarına göre 1 gün sonra sevk edilir
        public  void setShippedDate()
        {
            ShippedDate = DateTime.Now.AddDays(1);
        }

        // İstenen Tarih Bugunun Tarihinden 12 saat önce olmamalıdır.
        public void setRequiredDate(DateTime requiredDate)
        {
            OrderRuleCheck.CheckRequiredDate(requiredDate);
            RequiredDate = requiredDate;
        }

        public void SetId(Guid orderId)
        {
            TypeDataCheck.checkId(orderId);
            Id = orderId;
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case OrderAddEvent x: OnCreatedOrder(x); break;
                case OrderUpdateEvent x: OnUpdateOrder(x); break;
                case OrderRemoveEvent x: OnRemovedOrder(x); break;
            }
        }

        private void OnRemovedOrder(OrderRemoveEvent @event)
        {
            Id = @event.Id;
        }

        private void OnUpdateOrder(OrderUpdateEvent @event)
        {
            Id = @event.Id;
            CustomerID = @event.CustomerID;
            EmployeeID = @event.EmployeeID;
            OrderDate = @event.OrderDate;
            RequiredDate = @event.RequiredDate;
            ShippedDate = @event.ShippedDate;
            ShipName = @event.ShipName;
            ShipAddress = @event.ShipAddress;
            ShipCity = @event.ShipCity;
            ShipPostalCode = @event.ShipPostalCode;
            ShipCountry = @event.ShipCountry;

        }


        private void OnCreatedOrder(OrderAddEvent @event)
        {
            Id = @event.Id;
            CustomerID = @event.CustomerID;
            EmployeeID = @event.EmployeeID;
            OrderDate = @event.OrderDate;
            RequiredDate = @event.RequiredDate;
            ShippedDate = @event.ShippedDate;
            ShipName = @event.ShipName;
            ShipAddress = @event.ShipAddress;
            ShipCity = @event.ShipCity;
            ShipPostalCode = @event.ShipPostalCode;
            ShipCountry = @event.ShipCountry;
        }
    }
}
