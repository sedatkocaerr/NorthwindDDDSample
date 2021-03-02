using MediatR;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Domain.Validation.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.CustomersCommands
{
    public class CustomerUpdateCommand : CustomerCommand, IRequest<Customer>
    {
        public CustomerUpdateCommand(Guid id,string companyName, string contactName, string contactTitle,string email, string address
           ,string city, string postalCode, string country, string phone)
        {
            Id = id;
            CompanyName = companyName;
            ContactName = contactName;
            ContactTitle = contactTitle;
            Email = email;
            Address = address;
            PostalCode = postalCode;
            City = city;
            Country = country;
            Phone = phone;
        }

        public override bool IsValid()
        {
            ValidationResult = new CustomerUpdateValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
