using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.SupplierEvents
{
    public class SupplierRemoveEvent:Event
    {
        public SupplierRemoveEvent(Guid Id)
        {
            this.Id = Id;
        }
        public Guid Id { get; protected set; }
    }
}
