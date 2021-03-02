using NorthwindApi.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Domain.Orders;
using NorthwindApi.Domain.Events.EmployeesEvents;

namespace NorthwindApi.Domain.Domain.Employees
{
    public class Employee:Entity,IAggregateRoot
    {
        private List<Order> _orders;

        protected Employee()
        {
            _orders = new List<Order>();
        }


        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Title { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime HireDate { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        public virtual IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();


        public  Employee (Guid id,string firstName, string lastName, string title,
            DateTime birthDate, DateTime hireDate, string address, string city,
            string postalCode, string country)
        {
            SetId(id);
            SetFirstName(firstName);
            SetLastName(lastName);
            SetTitle(title);
            SetBrithDate(birthDate);
            SetHireDate(hireDate);

            TypeDataCheck.IsNullOrEmpty(address);
            TypeDataCheck.IsNullOrEmpty(city);
            TypeDataCheck.IsNullOrEmpty(postalCode);
            TypeDataCheck.IsNullOrEmpty(country);

            Address = address;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        public void SetId(Guid employeeId)
        {
            TypeDataCheck.checkId(employeeId);
            Id = employeeId;
        }

        public void SetFirstName(string firstName)
        {
            TypeDataCheck.IsNullOrEmpty(firstName);
            EmployeeRuleCheck.CheckEmployeeFirstName(firstName);
            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            TypeDataCheck.IsNullOrEmpty(lastName);
            EmployeeRuleCheck.CheckEmployeeLastName(lastName);
            LastName = lastName;
        }

        public void SetTitle(string title)
        {
            TypeDataCheck.IsNullOrEmpty(title);
            EmployeeRuleCheck.CheckEmployeeTitle(title);
            Title = title;
        }

        public void SetBrithDate(DateTime brithDate)
        {
            EmployeeRuleCheck.CheckEmployeeBirthDate(brithDate);
            BirthDate = brithDate;
        }

        public void SetHireDate(DateTime hireDate)
        {
            EmployeeRuleCheck.CheckEmployeeHireDate(hireDate);
            HireDate = hireDate;
        }

        public void AddOrder(Order order)
        {
            TypeDataCheck.IsNull(order);
            _orders.Add(order);
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case EmployeeAddEvent x: OnCreatedEmployee(x); break;
                case EmployeeUpdateEvent x: OnUpdateEmployee(x); break;
                case EmployeeRemoveEvent x: OnRemovedEmployee(x); break;
            }
        }

        private void OnRemovedEmployee(EmployeeRemoveEvent @event)
        {
            Id = @event.Id;
        }

        private void OnUpdateEmployee(EmployeeUpdateEvent @event)
        {
            Id = @event.Id;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            Title = @event.Title;
            BirthDate = @event.BirthDate;
            HireDate = @event.HireDate;
            Address = @event.Address;
            City = @event.City;

        }

        private void OnCreatedEmployee(EmployeeAddEvent @event)
        {
            Id = @event.Id;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            Title = @event.Title;
            BirthDate = @event.BirthDate;
            HireDate = @event.HireDate;
            Address = @event.Address;
            City = @event.City;
        }
    }
}
