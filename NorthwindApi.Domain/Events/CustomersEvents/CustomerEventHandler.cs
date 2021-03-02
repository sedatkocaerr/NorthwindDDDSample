using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Events.CustomersEvents
{
    public class CustomerEventHandler : INotificationHandler<CustomerAddEvent>,
      INotificationHandler<CustomerUpdateEvent>,
      INotificationHandler<CustomerRemoveEvent>
    {

        // Implement Customer Repository

        public CustomerEventHandler()
        {

        }

        public Task Handle(CustomerAddEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(CustomerUpdateEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(CustomerRemoveEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
