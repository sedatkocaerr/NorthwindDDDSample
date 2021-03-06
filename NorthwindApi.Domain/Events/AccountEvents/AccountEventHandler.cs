using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Events.AccountEvents
{
    public class AccountEventHandler :
        INotificationHandler<AccountRegisterEvent>,
        INotificationHandler<AccountUpdateEvent>

    {
        public Task Handle(AccountRegisterEvent notification, CancellationToken cancellationToken)
        {
            // send mail..
            throw new NotImplementedException();
        }

        public Task Handle(AccountUpdateEvent notification, CancellationToken cancellationToken)
        {
            // send notification..
            throw new NotImplementedException();
        }
    }
}
