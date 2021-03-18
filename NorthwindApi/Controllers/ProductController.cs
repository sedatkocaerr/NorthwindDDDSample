﻿using Microsoft.AspNetCore.Http;
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
    public class ProductController : ControllerBase
    {
        private IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _productAppService.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] ProductViewModel productViewModel)
        {
            var data = await _productAppService.AddProduct(productViewModel);
            if (data.ValidationResult.IsValid)
            {
                return Ok(data);
            }
            return BadRequest(data.ValidationResult);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _productAppService.GetAll();
            return Ok(data);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] ProductViewModel productViewModel)
        {
            var data = await _productAppService.UpdateProduct(productViewModel);
            if (data.ValidationResult.IsValid)
            {
                return Ok(data);
            }
            return BadRequest(data.ValidationResult);
        }


        [HttpDelete]
        [Route("Remove")]
        [Authorize]
        public async Task<IActionResult> Remove(Guid Id)
        {
            var data = await _productAppService.Remove(Id);
            if (data.ValidationResult.IsValid)
            {
                return Ok(data);
            }
            return BadRequest(data);
        }
    }
}
