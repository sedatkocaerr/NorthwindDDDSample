using MediatR;
using NorthwindApi.Domain.Domain.Suppliers;
using NorthwindApi.Domain.Validation.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Commands.SuppliersCommands
{
    public class SupplierAddCommand: SupplierCommand,IRequest<Supplier>
    {
        public SupplierAddCommand(string companyName, string contactName, string contactTitle, string adress,
            string city, string country, string phone)
        {
            CompanyName = companyName;
            ContactName = contactName;
            ContactTitle = contactTitle;
            Adress = adress;
            City = city;
            Country = country;
            Phone = phone;
        }

        public override bool IsValid()
        {
            CommandResponse.ValidationResult = new SupplierAddValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }
    }
}
