using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Events.Orders
{
    public class OrderEventHandler : INotificationHandler<OrderAddEvent>,
        INotificationHandler<OrderRemoveEvent>,
        INotificationHandler<OrderUpdateEvent>
    {
        public Task Handle(OrderUpdateEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(OrderRemoveEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(OrderAddEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
