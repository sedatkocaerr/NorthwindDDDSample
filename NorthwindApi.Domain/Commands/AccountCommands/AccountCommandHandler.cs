﻿using FluentValidation.Results;
using MediatR;
using NorthwindApi.Domain.Core;
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
        IRequestHandler<AccountRegisterCommand, ValidationResult>,
        IRequestHandler<AccountUpdateCommand, ValidationResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventSourceRepository _eventStoreRepository;

        public AccountCommandHandler(IAccountRepository accountRepository, IEventSourceRepository eventStoreRepository)
        {
            _accountRepository = accountRepository;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<ValidationResult> Handle(AccountRegisterCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid()) return request.ValidationResult;

            if (await _accountRepository.EmailExists(request.Email))
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("", "E-mail already exists."));
                return request.ValidationResult;
            }
           
            var account = new Account(Guid.NewGuid(), request.Name, request.SurName, request.Email, DateTime.Now);
            
            account.Apply(new AccountRegisterEvent(account.Id,account.Name,account.Surname,account.Email));
            
            await _accountRepository.Add(account);

            await _eventStoreRepository.SaveAsync<Account>(account);
            return await Commit(_accountRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AccountUpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid()) return request.ValidationResult;

            var checkAccount = await _accountRepository.FindById(request.Id);
            if (checkAccount==null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("", "Account Id Not Found."));
                return request.ValidationResult;
            }

            
            if (checkAccount.Email!= request.Email)
            {
                if(await _accountRepository.EmailExists(request.Email))
                {
                    request.ValidationResult.Errors.Add(new ValidationFailure("", "E-mail already exists."));
                    return request.ValidationResult;
                }
            }
            
            var account = new Account(Guid.NewGuid(), request.Name, request.SurName, request.Email, DateTime.Now);
            
            account.Apply(new AccountUpdateEvent(account.Id, account.Name, account.Surname, account.Email));
            
            _accountRepository.Update(account);

            await _eventStoreRepository.SaveAsync<Account>(account);
            return await Commit(_accountRepository.UnitOfWork);
        }
    }
}