using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Core.Command;

namespace NorthwindApi.Domain.Command
{
    public abstract class Command: IRequest<CommandResponse>, IBaseRequest
    {
        public CommandResponse CommandResponse { get; set; }
        public Command()
        {
            CommandResponse = new CommandResponse();
        }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}


