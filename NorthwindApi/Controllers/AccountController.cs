using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindApi.Application.Authentication.Abstract;
using NorthwindApi.Application.Authentication.Concrete;
using NorthwindApi.Application.Authentication.Request;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Application.ViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountAppService _accountAppService;
        private readonly IUserTokenAppService _userTokenService;

        public AccountController(IAccountAppService accountAppService, IUserTokenAppService userTokenService)
        {
            _accountAppService = accountAppService;
            _userTokenService = userTokenService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AccountRegisterViewModel accountViewModel)
        {
            var data = await _accountAppService.AddAccount(accountViewModel);
            return Ok(data);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest authenticateRequest)
        {
           var checkAccount =  await _accountAppService.CheckAccount(authenticateRequest);
            if (!checkAccount.status)
                return BadRequest(checkAccount);

           var tokenData = await _userTokenService.GenerateToken(authenticateRequest);
           return Ok(tokenData);
        }

        [HttpGet]
        [Route("Get")]
        [Helper.Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _accountAppService.GetById(id);
            return Ok(data);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] AccountViewModel accountUpdateViewModel)
        {
            var data = await _accountAppService.UpdateAccount(accountUpdateViewModel);
            return Ok(data);
        }
    }
}
