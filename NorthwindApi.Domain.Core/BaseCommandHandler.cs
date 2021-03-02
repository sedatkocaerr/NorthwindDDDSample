using FluentValidation.Results;
using NorthwindApi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Core
{
    public abstract class BaseCommandHandler
    {
        protected ValidationResult validationResult;
       
        public BaseCommandHandler()
        {
            validationResult = new ValidationResult();
        }

        protected async Task<ValidationResult> Commit(IUnitOfWork uow)
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if(await uow.Commit())
            {
                return validationResult;
            }

            validationResult.Errors.Add(new ValidationFailure(null, "An error occurred while saving data"));
            return validationResult;
        }
    }
}
