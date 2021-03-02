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
        public async Task<IActionResult> Add([FromBody] EmployeeViewModel EmployeeViewModel)
        {
            var data = await _employeeAppService.AddEmployee(EmployeeViewModel);
            return Ok(data);
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
        public async Task<IActionResult> Update([FromBody] EmployeeViewModel EmployeeViewModel)
        {
            var data = await _employeeAppService.UpdateEmployee(EmployeeViewModel);
            return Ok(data);
        }

        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> Remove(Guid Id)
        {
            var data = await _employeeAppService.Remove(Id);
            return Ok(data);
        }

    }
}
