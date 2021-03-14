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
    public class ShipperController : ControllerBase
    {

        private IShipperAppService _shipperAppService;

        public ShipperController(IShipperAppService shipperAppService)
        {
            _shipperAppService = shipperAppService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _shipperAppService.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] ShipperViewModel ShipperViewModel)
        {
            var data = await _shipperAppService.AddShipper(ShipperViewModel);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _shipperAppService.GetAll();
            return Ok(data);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] ShipperViewModel ShipperViewModel)
        {
            var data = await _shipperAppService.UpdateShipper(ShipperViewModel);
            return Ok(data);
        }

        [HttpDelete]
        [Route("Remove")]
        [Authorize]
        public async Task<IActionResult> Remove(Guid Id)
        {
            var data = await _shipperAppService.Remove(Id);
            return Ok(data);
        }
    }
}
