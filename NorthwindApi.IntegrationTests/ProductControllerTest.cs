using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Nest;
using NorthwindApi.Application.ElasticSearchServices.Settings;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Repository;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.Categories;
using NorthwindApi.Domain.Domain.Products;
using NorthwindApi.Domain.Domain.Suppliers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NorthwindApi.IntegrationTests
{
    [TestFixture]
    public class ProductControllerTest : IntegrationBase
    {
        private IProductRepository _productRepository;

        [SetUp]
        public void SetUp()
        {
            _productRepository = new ProductRepository(GetContext());
        }

        [Test]
        public async Task Must_Add_Valid_Product()
        {
            var category = await CreateCategory();
            var supplier = await CreateSupplier();

          
            var productViewModel = new ProductViewModel
            {
                CategoryID = category.Id,
                SupplierID = supplier.Id,
                ProductName = "3MM Rope Chain Necklace",
                QuantityPerUnit = "1",
                UnitPrice = 10,
                UnitsInStock = 10
            };
            var response = await _httpTestClient.PostAsJsonAsync("/api/product/add", productViewModel);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var productResponse = await response.Content.ReadAsAsync<CommandResponse>();

            (_productRepository.GetAll().Result).Count().Should().Be(1);

        }

        [Test]
        public async Task Must_Not_Add_Valid_Product()
        {
            var category = await CreateCategory();
            var supplier = await CreateSupplier();


            var productViewModel = new ProductViewModel
            {
                CategoryID = category.Id,
                SupplierID = supplier.Id,
                ProductName = string.Empty,
                QuantityPerUnit = "1",
                UnitPrice = 10,
                UnitsInStock = 10
            };
            var response = await _httpTestClient.PostAsJsonAsync("/api/product/add", productViewModel);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Must_Update_Valid_Product()
        {
            var product = await CreateProduct();

            //Arrange
            var productViewModel = new ProductViewModel 
            {
                Id=product.Id,
                CategoryID = product.CategoryID,
                SupplierID = product.SupplierID,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            };

            //Act
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/product/update", productViewModel).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            var updatedProduct = await _productRepository.FindById(product.Id);

            updatedProduct.ProductName.Should().Be(product.ProductName);
        }

        [Test]
        public async Task Must_Not_Update_Valid_Product()
        {
            var product = await CreateProduct();

            //Arrange
            var productViewModel = new ProductViewModel
            {
                Id = product.Id,
                CategoryID = product.CategoryID,
                SupplierID = product.SupplierID,
                ProductName = string.Empty,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            };

            //Act
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/product/update", productViewModel).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private async Task<Category> CreateCategory()
        {
            var category = new Category(Guid.NewGuid(),"Necklages", "Check out our necklaces selection.");
            var categoryRepository = new CategoryRepository(GetContext());
            await categoryRepository.Add(category);
            await categoryRepository.UnitOfWork.Commit();
            return category;
        }

        private async Task<Supplier> CreateSupplier()
        {
            var supplier = new Supplier(Guid.NewGuid(),"Bulk Supply","Sedat Kocaer","Purchasing Manager",
                "49 Gilbert St.","Istanbul","TR","(171) 555-2222");
            var supplierRepository = new SupplierRepository(GetContext());
            await supplierRepository.Add(supplier);
            await supplierRepository.UnitOfWork.Commit();
            return supplier;
        }   
        
        private async Task<Product> CreateProduct()
        {
            var category = await CreateCategory();
            var supplier = await CreateSupplier();

            var product = new Product(Guid.NewGuid(),category,supplier,"3MM Rope Chain Necklace","1",10,10);
            await _productRepository.Add(product);
            await _productRepository.UnitOfWork.Commit();
            return product;
        }
    }                                   
}                                       
                                        
                                        
                                        