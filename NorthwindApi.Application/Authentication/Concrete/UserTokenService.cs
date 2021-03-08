using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NorthwindApi.Application.Authentication.Abstract;
using NorthwindApi.Application.Authentication.Request;
using NorthwindApi.Application.Authentication.Response;
using NorthwindApi.Application.Authentication.Settings;
using NorthwindApi.Domain.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Authentication.Concrete
{
    public class UserTokenService : IUserTokenAppService
    {
        private readonly JwtSetting _appSettingsTokenJwt;

        private readonly IAccountRepository _accountRepository;

        public UserTokenService(IOptions<JwtSetting> appSettingsTokenJwt, IAccountRepository accountRepository)
        {
            _appSettingsTokenJwt = appSettingsTokenJwt.Value;
            _accountRepository = accountRepository;
        }

        public async Task<BaseResponse<AuthenticateResponse>> GenerateToken(AuthenticateRequest authenticateRequest)
        {
            var checkAccount = await _accountRepository.FindOne(x => x.Email == authenticateRequest.Email);
            if (checkAccount == null)
            {
                return new BaseResponse<AuthenticateResponse>(null, false, "E-mail Not Found.");
            }
           
            var token = generateJwtToken(checkAccount);
            return new BaseResponse<AuthenticateResponse>(new AuthenticateResponse(checkAccount.Id,
                checkAccount.Name,checkAccount.Email, token), true);
        }

        public async Task<Account> GetAccountById(Guid Id)
        {
           return await _accountRepository.FindById(Id);
        }

        private string generateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettingsTokenJwt.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(SetClaims(account)),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private IEnumerable<Claim> SetClaims(Account account)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", account.Id.ToString()));
            claims.Add(new Claim("name", account.Name));
            claims.Add(new Claim("email", account.Email));
            return claims;
        }
    }
}
