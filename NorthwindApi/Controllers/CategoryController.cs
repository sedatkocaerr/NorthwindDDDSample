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
    public class CategoryController : ControllerBase
    {
        private ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _categoryAppService.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] CategoryViewModel categoryViewModel)
        {
            var data = await _categoryAppService.AddCategory(categoryViewModel);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _categoryAppService.GetAll();
            return Ok(data);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] CategoryViewModel categoryViewModel)
        {
            var data = await _categoryAppService.UpdateCategory(categoryViewModel);
            return Ok(data);
        }

        [HttpDelete]
        [Route("Remove")]
        [Authorize]
        public async Task<IActionResult> Remove(Guid Id)
        {
            var data = await _categoryAppService.Remove(Id);
            return Ok(data);
        }
    }
}
