using MediatR;
using NorthwindApi.Domain.Command;
using NorthwindApi.Domain.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Commands.EmployeesCommands
{
    public class EmployeesCommand:Command,IRequest<Employee>
    {
        public Guid Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Title { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public DateTime HireDate { get; protected set; }
        public string Address { get; protected set; }
        public string City { get; protected set; }
        public string PostalCode { get; protected set; }
        public string Country { get; protected set; }
        public string PhotoPath { get; protected set; }
    }
}
