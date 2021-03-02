using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.CustomersEvents
{
    public class CustomerRemoveEvent:Event
    {

        public CustomerRemoveEvent(Guid Id)
        {
            this.Id = Id;
        }
        public Guid Id { get; set; }

    }
}
