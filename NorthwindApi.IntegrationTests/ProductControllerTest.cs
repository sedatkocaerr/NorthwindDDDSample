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
    public class ProductControllerTest : IntegrationBase
    {
        
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

        [Fact]
        public async Task Get_ReturnsPost_WhenProductExistsInTheDatabase()
        {
            //Arrange
            var prouctModel = await CreateProduct();

            //Act
            var responseProductList = await _httpTestClient.GetAsync("/api/product/Get?Id="+prouctModel.Id);

            // Assert
            responseProductList.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var productData = await responseProductList.Content.ReadAsAsync<ProductViewModel>();

            productData.Id.Should().Be(prouctModel.Id);
            productData.ProductName.Should().Be(prouctModel.ProductName);
            productData.CategoryID.Should().Be(prouctModel.CategoryID);
            productData.SupplierID.Should().Be(prouctModel.SupplierID);
            productData.UnitPrice.Should().Be(prouctModel.UnitPrice);
            productData.QuantityPerUnit.Should().Be(prouctModel.QuantityPerUnit);
        }
    }
}
