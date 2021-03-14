

using MediatR;
using NorthwindApi.Application.Validation.Customers;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.Customers;

namespace NorthwindApi.Application.Commands.CustomersCommands
{
    public class CustomerAddCommand: CustomerCommand, IRequest<Customer>
    {
        public CustomerAddCommand(string companyName, string contactName, string contactTitle, string email, string address
            , string city, string postalCode, string country, string phone)
        {
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
            CommandResponse.ValidationResult = new CustomerAddValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
