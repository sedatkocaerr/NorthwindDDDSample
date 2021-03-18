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
    public class EmployeeController : ControllerBase
    {
        private IEmployeeAppService _employeeAppService;

        public EmployeeController(IEmployeeAppService employeeAppService)
        {
            this._employeeAppService = employeeAppService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _employeeAppService.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] EmployeeViewModel EmployeeViewModel)
        {
            var data = await _employeeAppService.AddEmployee(EmployeeViewModel);
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
            var data = await _employeeAppService.GetAll();
            return Ok(data);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] EmployeeViewModel EmployeeViewModel)
        {
            var data = await _employeeAppService.UpdateEmployee(EmployeeViewModel);
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
            var data = await _employeeAppService.Remove(Id);
            return Ok(data);
        }

    }
}
