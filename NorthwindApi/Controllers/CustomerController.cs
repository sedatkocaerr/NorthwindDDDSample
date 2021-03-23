using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerAppService _customerAppService;

        public CustomerController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
           var data = await _customerAppService.GetById(id);
           return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] CustomerViewModel customerViewModel)
        {
            var data = await _customerAppService.AddCustomer(customerViewModel);
            if(data.ValidationResult.IsValid)
            {
                return Ok(data);
            }
            return BadRequest(data);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _customerAppService.GetAll();
            return Ok(data);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] CustomerViewModel customerViewModel)
        {
            var data = await _customerAppService.UpdateCustomer(customerViewModel);
            if (data.ValidationResult.IsValid)
            {
                return Ok(data);
            }
            return BadRequest(data);
        }

        [HttpDelete]
        [Route("Remove")]
        [Authorize]
        public async Task<IActionResult> Remove(Guid Id)
        {
            var data = await _customerAppService.Remove(Id);
            return Ok(data);
        }
    }
}
