using FluentValidation.Results;
using NorthwindApi.Application.Authentication.Request;
using NorthwindApi.Application.Authentication.Response;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Application.ViewModels.AccountViewModels;
using NorthwindApi.Domain.Core.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task<AccountViewModel> GetById(Guid id);
        Task<CommandResponse> AddAccount(AccountRegisterViewModel AccountViewModel);

        Task<BaseResponse<bool>> CheckAccount(AuthenticateRequest accountRegisterViewModel);

        Task<CommandResponse> UpdateAccount(AccountViewModel AccountUpdateViewModel);
    }
}
