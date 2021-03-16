using FluentValidation.Results;
using NorthwindApi.Data.Ef;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Commands.Handler
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
            if (uow == null) throw new ArgumentNullException(nameof(uow));

            using (var transaction = await uow.BeginTransactionAsync())
            {
                commandResponse.ValidationResult = new ValidationResult();
                if (await uow.Commit(transaction))
                {
                    return commandResponse;
                }

                commandResponse.ValidationResult.Errors.Add(new ValidationFailure(null, "An error occurred while saving data"));
                return commandResponse;
            }
            
        }

        protected async Task<CommandResponse> Commit(IUnitOfWork uow,Guid Id)
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            using (var transaction = await uow.BeginTransactionAsync())
            {
                commandResponse.ValidationResult = new ValidationResult();
                if (await uow.Commit(transaction))
                {
                    commandResponse.Id = Id;
                    return commandResponse;
                }
                commandResponse.ValidationResult.Errors.Add(new ValidationFailure(null, "An error occurred while saving data"));
                return commandResponse;
            }
        }
    }
}
