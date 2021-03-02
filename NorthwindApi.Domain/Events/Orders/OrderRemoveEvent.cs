using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.Orders
{
    public class OrderRemoveEvent:Event
    {
        public OrderRemoveEvent(Guid Id)
        {
            this.Id = Id;
        }

        public Guid Id { get; private set; }
    }
}
