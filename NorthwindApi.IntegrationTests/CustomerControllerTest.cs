using FluentAssertions;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Repository;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.Customers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.IntegrationTests
{
    public class CustomerControllerTest:IntegrationBase
    {
        private ICustomersRepository _customersRepository;

        [SetUp]
        public void SetUp()
        {
            _customersRepository = new CustomersRepository(GetContext());
        }

        [Test]
        public async Task Must_Add_Valid_Customer()
        {
            await AuthanticateAccountAsync();

            var customerViewModel = new CustomerViewModel
            {
               CompanyName= "Alfreds Futterkiste",
               ContactName= "Maria Anders",
               ContactTitle= "Sales Representative",
               Address= "Obere Str. 57",
               City= "Berlin",
               Country= "Germany",
               Phone= "030-0074321",
               Email= "MariaAnders@northwind.com",
               PostalCode= "12209"
            };
            var response = await _httpTestClient.PostAsJsonAsync("/api/customer/add", customerViewModel);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (_customersRepository.GetAll().Result).Count().Should().Be(1);
        }

        [Test]
        public async Task Must_Not_Add_Valid_Customer()
        {
            await AuthanticateAccountAsync();

            var customerViewModel = new CustomerViewModel
            {
                CompanyName = string.Empty,
                ContactName = "Maria Anders",
                ContactTitle = "Sales Representative",
                Address = "Obere Str. 57",
                City = "Berlin",
                Country = "Germany",
                Phone = "030-0074321",
                Email = "MariaAnders@northwind.com",
                PostalCode = "12209"
            };
            var response = await _httpTestClient.PostAsJsonAsync("/api/customer/add", customerViewModel);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            (_customersRepository.GetAll().Result).Count().Should().Be(1);
        }

        [Test]
        public async Task Must_Update_Valid_Customer()
        {
            await AuthanticateAccountAsync();

            var customer = await CreateCustomer();

            //Arrange
            var customerViewModel = new CustomerViewModel
            {
                Id=customer.Id,
                CompanyName = "Updated CompanyName",
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                Phone = customer.Phone,
                Email = customer.Email,
                PostalCode = customer.PostalCode
            };

            //Act
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/customer/update", customerViewModel).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            var updatedCustomer= await _customersRepository.FindById(customerViewModel.Id);

            updatedCustomer.CompanyName.Should().Be(customerViewModel.CompanyName);
        }


        [Test]
        public async Task Must_Not_Update_Valid_Customer()
        {
            await AuthanticateAccountAsync();

            var customer = await CreateCustomer();

            //Arrange
            var customerViewModel = new CustomerViewModel
            {
                Id = customer.Id,
                CompanyName = string.Empty,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                Phone = customer.Phone,
                Email = customer.Email,
                PostalCode = customer.PostalCode
            };

            //Act
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/customer/update", customerViewModel).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var updatedCustomer = await _customersRepository.FindById(customerViewModel.Id);

            updatedCustomer.CompanyName.Should().Be(customer.CompanyName);
        }

        private async Task<Customer> CreateCustomer()
        {
            var customer = new Customer
            (Guid.NewGuid(), "Alfreds Futterkiste", "Maria Anders", "Sales Representative","sedatk@hotmail.com", "Obere Str. 57",
            "Berlin", "12209", "Germany","05420000000");
             _customersRepository.Add(customer);
            await _customersRepository.UnitOfWork.Commit();
            return customer;
        }
    }
}
