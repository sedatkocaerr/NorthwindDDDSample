using NorthwindApi.Domain.Commands.SuppliersCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Suppliers
{
    public class SupplierAddValidation:SupplierValidation<SupplierAddCommand>
    {
        public SupplierAddValidation()
        {
            validateCompanyName();
            validateContactName();
            validateContactTitle();
            validateAdress();
            validateCity();
            validateCountry();
            validatePhone();
        }
    }
}
