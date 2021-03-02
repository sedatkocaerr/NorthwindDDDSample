using NorthwindApi.Domain.Commands.SuppliersCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Validation.Suppliers
{
    public class SupplierRemoveValidation:SupplierValidation<SupplierRemoveCommand>
    {
        public SupplierRemoveValidation()
        {
            validateId();
        }
    }
}
