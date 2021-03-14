using MediatR;
using NorthwindApi.Domain.Domain.Suppliers;
using NorthwindApi.Domain.Validation.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.Commands.SuppliersCommands
{
    public class SupplierUpdateCommand : SupplierCommand, IRequest<Supplier>
    {
        public SupplierUpdateCommand(Guid id,string companyName, string contactName, string contactTitle, string adress,
            string city, string country, string phone)
        {
            Id = id;
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
            CommandResponse.ValidationResult = new SupplierUpdateValidation().Validate(this);
            return CommandResponse.ValidationResult.IsValid;
        }

    }
}
