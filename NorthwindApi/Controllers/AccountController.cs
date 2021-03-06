using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
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

        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AccountViewModel accountViewModel)
        {
            var data = await _accountAppService.AddAccount(accountViewModel);
            return Ok(data);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _accountAppService.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] AccountViewModel accountViewModel)
        {
            var data = await _accountAppService.UpdateAccount(accountViewModel);
            return Ok(data);
        }
    }
}
