using AutoMapper;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Mediator;
using NorthwindApi.Domain.Domain.Accounts;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Text;
using System.Threading.Tasks;
using NorthwindApi.Application.ViewModels.AccountViewModels;
using NorthwindApi.Domain.Core.Security;
using NorthwindApi.Application.Authentication.Request;
using NorthwindApi.Application.Authentication.Response;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Application.Commands.AccountCommands;

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

        public async Task<CommandResponse> AddAccount(AccountRegisterViewModel accountViewModel)
        {
            var account = _mapper.Map<AccountRegisterCommand>(accountViewModel);
            return await _mediatorHandler.SendCommand<AccountRegisterCommand>(account);
        }

        public async Task<BaseResponse<bool>> CheckAccount(AuthenticateRequest accountRegisterViewModel)
        {
            var account = await _accountRepository.FindOne(x => x.Email == accountRegisterViewModel.Email);
            if (account != null)
            {
                if (HashingHelper.VerifyPasswordHash(accountRegisterViewModel.Password,
                    account.PasswordHash, account.PasswordSalt))
                {
                    return new BaseResponse<bool>(true,true);
                }
                return new BaseResponse<bool>(false, false,"E-Mail Or Password wrong.");
            }
            return new BaseResponse<bool>(false, false, "E-Mail Or Password wrong.");
        }

        public async Task<AccountViewModel> GetById(Guid id)
        {
            var account = await _accountRepository.FindById(id);
            return _mapper.Map<AccountViewModel>(account);
        }

        public async Task<CommandResponse> UpdateAccount(AccountViewModel accountUpdateViewModel)
        {
            var account = _mapper.Map<AccountUpdateCommand>(accountUpdateViewModel);
            return await _mediatorHandler.SendCommand<AccountUpdateCommand>(account);
        }
    }
}
