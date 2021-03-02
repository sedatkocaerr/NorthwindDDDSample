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
    public class SupplierController : ControllerBase
    {
        private ISupplierAppService _supplierAppService;

        public SupplierController(ISupplierAppService supplierAppService)
        {
            _supplierAppService = supplierAppService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _supplierAppService.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] SupplierViewModel supplierViewModel)
        {
            var data = await _supplierAppService.AddSupplier(supplierViewModel);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _supplierAppService.GetAll();
            return Ok(data);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] SupplierViewModel supplierViewModel)
        {
            var data = await _supplierAppService.UpdateSupplier(supplierViewModel);
            return Ok(data);
        }

        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> Remove(Guid Id)
        {
            var data = await _supplierAppService.Remove(Id);
            return Ok(data);
        }
    }
}
