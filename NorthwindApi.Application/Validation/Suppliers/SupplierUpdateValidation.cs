using NorthwindApi.Application.Commands.SuppliersCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Suppliers
{
    public class SupplierUpdateValidation:SupplierValidation<SupplierUpdateCommand>
    {
        public SupplierUpdateValidation()
        {
            validateId();
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
