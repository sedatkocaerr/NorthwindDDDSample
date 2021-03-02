using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.EmployeesEvents
{
    public class EmployeeAddEvent:Event
    {
        public EmployeeAddEvent(Guid Id, string firstName, string lastName, string title, DateTime birthDate, DateTime hireDate, string address, string city)
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

        public Guid Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Title { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public DateTime HireDate { get; protected set; }
        public string Address { get; protected set; }
        public string City { get; protected set; }
    }
}
