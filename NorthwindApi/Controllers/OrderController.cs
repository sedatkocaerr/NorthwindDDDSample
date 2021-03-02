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
    public class OrderController : ControllerBase
    {
        private IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            this._orderAppService = orderAppService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _orderAppService.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] OrderViewModel OrderViewModel)
        {
            var data = await _orderAppService.AddOrder(OrderViewModel);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _orderAppService.GetAll();
            return Ok(data);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] OrderViewModel OrderViewModel)
        {
            var data = await _orderAppService.UpdateOrder(OrderViewModel);
            return Ok(data);
        }

        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> Remove(Guid Id)
        {
            var data = await _orderAppService.Remove(Id);
            return Ok(data);
        }

        [HttpPost]
        [Route("addOrderDetail")]
        public async Task<IActionResult> AddOrderDetail([FromBody] OrderDetailViewModel OrderDetailViewModel)
        {
            var data = await _orderAppService.AddOrderDetail(OrderDetailViewModel);
            return Ok(data);
        }
    }
}
