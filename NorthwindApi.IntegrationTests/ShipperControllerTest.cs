using FluentAssertions;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Repository;
using NorthwindApi.Domain.Domain.Shippers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.IntegrationTests
{
    public class ShipperControllerTest:IntegrationBase
    {

        private IShippersRepository _shippersRepository;

        [SetUp]
        public void SetUp()
        {
            _shippersRepository = new ShippersRepository(GetContext());
        }

        [Test]
        public async Task Must_Valid_Add_Shipper()
        {
            await AuthanticateAccountAsync();

            var shipperViewModel = new ShipperViewModel()
            {
                CompanyName = "Speedy Express",
                Phone = "(503) 555 - 9831"
            };
            var httpResponseMessage = _httpTestClient.PostAsJsonAsync("/api/shipper/add", shipperViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Must_Not_Add_Valid_Shipper()
        {
            await AuthanticateAccountAsync();

            var shipperViewModel = new ShipperViewModel()
            {
                CompanyName = string.Empty,
                Phone = "(503) 555 - 9831"
            };
            var httpResponseMessage = _httpTestClient.PostAsJsonAsync("/api/shipper/add", shipperViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Test]
        public async Task Must_Update_Valid_Shipper()
        {
            await AuthanticateAccountAsync();

            var shipper = await CreateShipper();

            var shipperViewModel = new ShipperViewModel()
            {
                Id = shipper.Id,
                CompanyName = "Update Shipper",
                Phone = shipper.Phone,
            };
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/shipper/update", shipperViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Test]
        public async Task Must_Not_Update_Valid_Shipper()
        {
            await AuthanticateAccountAsync();

            var shipper = await CreateShipper();

            var shipperViewModel = new ShipperViewModel()
            {
                Id = shipper.Id,
                CompanyName = string.Empty,
                Phone = shipper.Phone,
            };
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/shipper/update", shipperViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        public async Task<Shipper> CreateShipper()
        {
            var shipper = new Shipper(Guid.NewGuid(), "Federal Shipping", "(503) 555-9931");
            await _shippersRepository.Add(shipper);
            await _shippersRepository.UnitOfWork.Commit();
            return shipper;
        }
    }
}
