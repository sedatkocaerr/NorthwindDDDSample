using NorthwindApi.Application.Authentication.Request;
using NorthwindApi.Application.Authentication.Response;
using NorthwindApi.Domain.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Authentication.Abstract
{
    public interface IUserTokenAppService
    {
        Task<BaseResponse<AuthenticateResponse>> GenerateToken(AuthenticateRequest authenticateRequest);
        Task<Account> GetAccountById(Guid Id);
    }
}
