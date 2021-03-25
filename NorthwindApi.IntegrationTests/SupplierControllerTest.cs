using FluentAssertions;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Repository;
using NorthwindApi.Domain.Domain.Suppliers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.IntegrationTests
{
    public class SupplierControllerTest:IntegrationBase
    {
        private ISupplierRepository _supplierRepository;

        [SetUp]
        public void SetUp()
        {
            _supplierRepository = new SupplierRepository(GetContext());
        }

        [Test]
        public async Task Must_Valid_Add_Supplier()
        {
            await AuthanticateAccountAsync();

            var supplierViewModel = new SupplierViewModel()
            {
                CompanyName= "Exotic Liquids",
                ContactName= "Charlotte Cooper",
                ContactTitle= "Purchasing Manager",
                Adress= "49 Gilbert St.",
                City= "London",
                Country= "UK",
                Phone= "(171) 555-2222"
            };
            var httpResponseMessage = _httpTestClient.PostAsJsonAsync("/api/supplier/add", supplierViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Must_Not_Add_Valid_Supplier()
        {
            await AuthanticateAccountAsync();

            var supplierViewModel = new SupplierViewModel()
            {
                CompanyName = string.Empty,
                ContactName = "Charlotte Cooper",
                ContactTitle = "Purchasing Manager",
                Adress = "49 Gilbert St.",
                City = "London",
                Country = "UK",
                Phone = "(171) 555-2222"
            };
            var httpResponseMessage = _httpTestClient.PostAsJsonAsync("/api/supplier/add", supplierViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Test]
        public async Task Must_Update_Valid_Supplier()
        {
            await AuthanticateAccountAsync();

            var supplier = await CreateSupplier();

            var supplierViewModel = new SupplierViewModel()
            {
                Id=supplier.Id,
                CompanyName = supplier.CompanyName,
                ContactName = "Updated Charlotte Cooper",
                ContactTitle = supplier.ContactTitle,
                Adress = supplier.Adress,
                City = supplier.City,
                Country = supplier.Country,
                Phone = supplier.Phone
            };
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/supplier/update", supplierViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Test]
        public async Task Must_Not_Update_Valid_Supplier()
        {
            await AuthanticateAccountAsync();

            var supplier = await CreateSupplier();

            var supplierViewModel = new SupplierViewModel()
            {
                Id = supplier.Id,
                CompanyName = supplier.CompanyName,
                ContactName = string.Empty,
                ContactTitle = supplier.ContactTitle,
                Adress = supplier.Adress,
                City = supplier.City,
                Country = supplier.Country,
                Phone = supplier.Phone
            };
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/supplier/update", supplierViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        public async Task<Supplier> CreateSupplier()
        {
            var supplier = new Supplier(Guid.NewGuid(), "New Orleans Cajun Delights", "Shelley Burke", "Order Administrator",
               "P.O. Box 78934", "New Orleans", "USA", "(100) 555-4822");
            await _supplierRepository.Add(supplier);
            await _supplierRepository.UnitOfWork.Commit();
            return supplier;
        }

    }
}
