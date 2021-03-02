using FluentValidation.Results;
using NorthwindApi.Domain.Commands;
using NorthwindApi.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Data.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
