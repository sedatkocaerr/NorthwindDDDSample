using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Events.ProductsEvents
{
    public class ProductEventHandler : INotificationHandler<ProductAddEvent>,
        INotificationHandler<ProductUpdateEvent>,
        INotificationHandler<ProductRemoveEvent>
    {
        public Task Handle(ProductRemoveEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(ProductUpdateEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(ProductAddEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
