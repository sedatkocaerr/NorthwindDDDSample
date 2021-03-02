using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.ProductsEvents
{
    public class ProductRemoveEvent : Event, INotification
    {
        public ProductRemoveEvent(Guid Id)
        {
            this.Id = Id;
        }

        public Guid Id { get; protected set; }
    }
}
