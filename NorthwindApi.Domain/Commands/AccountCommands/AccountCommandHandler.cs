using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Core.Security;
using NorthwindApi.Domain.Domain.Accounts;
using NorthwindApi.Domain.Events.AccountEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.Domain.Commands.AccountCommands
{
    public class AccountCommandHandler : BaseCommandHandler,
        IRequestHandler<AccountRegisterCommand, CommandResponse>,
        IRequestHandler<AccountUpdateCommand, CommandResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventSourceRepository _eventStoreRepository;

        public AccountCommandHandler(IAccountRepository accountRepository, IEventSourceRepository eventStoreRepository)
        {
            _accountRepository = accountRepository;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<CommandResponse> Handle(AccountRegisterCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            if (await _accountRepository.EmailExists(request.Email))
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure("", "E-mail already exists."));
                return request.CommandResponse;
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            var account = new Account(Guid.NewGuid(), request.Name, request.SurName, request.Email,
                passwordHash,passwordSalt, DateTime.Now);
            
            account.Apply(new AccountRegisterEvent(account.Id,account.Name,account.Surname,account.Email));
            
            await _accountRepository.Add(account);

            await _eventStoreRepository.SaveAsync<Account>(account);
            return await Commit(_accountRepository.UnitOfWork);
        }

        public async Task<CommandResponse> Handle(AccountUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResponse;

            var checkAccount = await _accountRepository.FindById(request.Id);
            if (checkAccount==null)
            {
                request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure("", "Account Id Not Found."));
                return request.CommandResponse;
            }

            
            if (checkAccount.Email!= request.Email)
            {
                if(await _accountRepository.EmailExists(request.Email))
                {
                    request.CommandResponse.ValidationResult.Errors.Add(new ValidationFailure("", "E-mail already exists."));
                    return request.CommandResponse;
                }
            }
            
            var account = new Account(request.Id, request.Name, request.SurName, request.Email,
                checkAccount.PasswordHash, checkAccount.PasswordSalt, DateTime.Now);
            
            account.Apply(new AccountUpdateEvent(account.Id, account.Name, account.Surname, account.Email));
            
            _accountRepository.Update(account);

            await _eventStoreRepository.SaveAsync<Account>(account);
            return await Commit(_accountRepository.UnitOfWork);
        }
    }
}
