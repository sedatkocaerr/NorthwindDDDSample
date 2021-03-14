using MediatR;
using NorthwindApi.Application.Validation.Customers;
using NorthwindApi.Domain.Domain.Customers;
using System;

namespace NorthwindApi.Application.Commands.CustomersCommands
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
            CommandResponse.ValidationResult = new CustomerUpdateValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
