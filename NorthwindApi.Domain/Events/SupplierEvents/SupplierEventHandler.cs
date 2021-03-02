using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Events.SupplierEvents
{
    public class SupplierEventHandler : INotificationHandler<SupplierAddEvent>,
        INotificationHandler<SupplierUpdateEvent>,
        INotificationHandler<SupplierRemoveEvent>

    {
        public Task Handle(SupplierAddEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(SupplierUpdateEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(SupplierRemoveEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
