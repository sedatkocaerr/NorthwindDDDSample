using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Commands;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Data.Mediator
{
    public  class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;


        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {

             await _mediator.Publish(@event);
        }

        public virtual async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
           return await _mediator.Send(command);
        }
    }
}
