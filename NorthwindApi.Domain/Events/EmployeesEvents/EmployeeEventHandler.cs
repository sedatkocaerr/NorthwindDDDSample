using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Events.EmployeesEvents
{
    public class EmployeeEventHandler : INotificationHandler<EmployeeAddEvent>,
        INotificationHandler<EmployeeUpdateEvent>,
        INotificationHandler<EmployeeRemoveEvent>
    {
        public Task Handle(EmployeeAddEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(EmployeeUpdateEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(EmployeeRemoveEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
