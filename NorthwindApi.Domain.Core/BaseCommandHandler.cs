using FluentValidation.Results;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Core
{
    public abstract class BaseCommandHandler
    {
        protected CommandResponse commandResponse;
       
        public BaseCommandHandler()
        {
            commandResponse = new CommandResponse();
        }

        protected async Task<CommandResponse> Commit(IUnitOfWork uow)
        {
            commandResponse.ValidationResult = new ValidationResult();
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (await uow.Commit())
            {
                return commandResponse;
            }

            commandResponse.ValidationResult.Errors.Add(new ValidationFailure(null, "An error occurred while saving data"));
            return commandResponse;
        }

        protected async Task<CommandResponse> Commit(IUnitOfWork uow,Guid Id)
        {
            commandResponse.ValidationResult = new ValidationResult();
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if(await uow.Commit())
            {
                commandResponse.Id = Id;
                return commandResponse;
            }

            commandResponse.ValidationResult.Errors.Add(new ValidationFailure(null, "An error occurred while saving data"));
            return commandResponse;
        }
    }
}
