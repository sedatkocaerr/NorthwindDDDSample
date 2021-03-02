using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.EmployeesEvents
{
    public class EmployeeUpdateEvent:Event
    {
        public EmployeeUpdateEvent(Guid Id, string firstName, string lastName, string title, DateTime birthDate, DateTime hireDate, string address, string city)
        {
            this.Id = Id;
            FirstName = firstName;
            LastName = lastName;
            Title = title;
            BirthDate = birthDate;
            HireDate = hireDate;
            Address = address;
            City = city;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Title { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime HireDate { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
    }
}
