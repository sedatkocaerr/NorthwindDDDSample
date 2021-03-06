using AutoMapper;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Mediator;
using NorthwindApi.Domain.Commands.AccountCommands;
using NorthwindApi.Domain.Domain.Accounts;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.AppServices
{
    public class AccountAppService : IAccountAppService
    {

        private IMapper _mapper;
        private IAccountRepository _accountRepository;
        private IMediatorHandler _mediatorHandler;


        public AccountAppService(IMapper mapper, IAccountRepository accountRepository, IMediatorHandler mediatorHandler)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<ValidationResult> AddAccount(AccountViewModel accountViewModel)
        {
            var account = _mapper.Map<AccountRegisterCommand>(accountViewModel);
            return await _mediatorHandler.SendCommand<AccountRegisterCommand>(account);
        }

        public async Task<AccountViewModel> GetById(Guid id)
        {
            var account = await _accountRepository.FindById(id);
            return _mapper.Map<AccountViewModel>(account);
        }

        public async Task<ValidationResult> UpdateAccount(AccountViewModel accountViewModel)
        {
            var account = _mapper.Map<AccountUpdateCommand>(accountViewModel);
            return await _mediatorHandler.SendCommand<AccountUpdateCommand>(account);
        }
    }
}
