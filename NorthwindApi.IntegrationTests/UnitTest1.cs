using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NorthwindApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NorthwindApi.IntegrationTests
{
    public class UnitTest1 : IntegrationBase
    {
        //private readonly WebApplicationFactory<Startup> _factory;
        //public UnitTest1(WebApplicationFactory<Startup> factory)
        //{
        //    _factory = factory;

        //}
        [Fact]
        public async Task GetAll_WithoutAnyProducts_ReturnsEmptyResponse()
        {
            //Arrange
            await AuthanticateAccountAsync();

            //Act
            var responseProductList = await _httpTestClient.GetAsync("/api/product/Getall");

            // Assert
            responseProductList.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            (await responseProductList.Content.ReadAsAsync<List<ProductViewModel>>()).Should().BeEmpty();
        }
    }
}
