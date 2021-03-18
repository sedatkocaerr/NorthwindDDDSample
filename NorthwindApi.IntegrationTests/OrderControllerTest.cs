using FluentAssertions;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Repository;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.Domain.Domain.Employees;
using NorthwindApi.Domain.Domain.Orders;
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
    [TestFixture]
    public class OrderControllerTest:IntegrationBase
    {
        private IOrderRepository _orderRepository;

        [SetUp]
        public void SetUp()
        {
            _orderRepository = new OrderRepository(GetContext());
        }

        [Test]
        public async Task  Must_Add_Valid_Order()
        {
            await AuthanticateAccountAsync();

            //create customer 
           var customer =  await CreateCustomer();

            //create Employee
           var employee = await CreateEmployee();

            //create Shipper
            var shipper = await CreateShipper();

            var orderViewModel = new OrderViewModel()
            {
                CustomerID= customer.Id,
                EmployeeID=employee.Id,
                OrderDate=DateTime.Now,
                RequiredDate=DateTime.Now.AddDays(10),
                ShippedDate= DateTime.Now.AddDays(1),
                ShipVia= shipper.Id,
                ShipName= "Vins et alcools Chevalier",
                ShipAddress= "59 rue de l'Abbaye",
                ShipCity="Istanbul",
                ShipPostalCode="34000",
                ShipCountry="Turkiye"
            };

            //Act
            var httpResponseMessage = _httpTestClient.PostAsJsonAsync("/api/order/add", orderViewModel).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        }


        [Test]
        public async Task Must_Not_Valid_Add_Order()
        {
            await AuthanticateAccountAsync();

            //create customer 
            var customer = await CreateCustomer();

            //create Employee
            var employee = await CreateEmployee();

            //create Shipper
            var shipper = await CreateShipper();

            var orderViewModel = new OrderViewModel()
            {
                CustomerID = customer.Id,
                EmployeeID = employee.Id,
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now.AddDays(10),
                ShippedDate = DateTime.Now.AddDays(1),
                ShipVia = shipper.Id,
                ShipName = string.Empty,
                ShipAddress = "59 rue de l'Abbaye",
                ShipCity = "Istanbul",
                ShipPostalCode = "34000",
                ShipCountry = "Turkiye"
            };

            //Act
            var httpResponseMessage = _httpTestClient.PostAsJsonAsync("/api/order/add", orderViewModel).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }


        [Test]
        public async Task Must_Update_Valid_Order()
        {
            await AuthanticateAccountAsync();

            var order = await CreateOrder();

            var orderViewModel = new OrderViewModel()
            {
                Id=order.Id,
                CustomerID = order.CustomerID,
                EmployeeID = order.EmployeeID,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipVia = order.ShipVia,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = "Cunewalde",
                ShipPostalCode = "01307",
                ShipCountry = "Germany"
            };

            //Act
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/order/update", orderViewModel).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        }


        [Test]
        public async Task Must_Update_Not_Valid_Order()
        {
            await AuthanticateAccountAsync();

            var order = await CreateOrder();

            var orderViewModel = new OrderViewModel()
            {
                Id = order.Id,
                CustomerID = order.CustomerID,
                EmployeeID = order.EmployeeID,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipVia = order.ShipVia,
                ShipName = string.Empty,
                ShipAddress = order.ShipAddress,
                ShipCity = "Cunewalde",
                ShipPostalCode = "01307",
                ShipCountry = "Germany"
            };

            //Act
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/order/update", orderViewModel).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        [Test]
        public async Task Must_Remove_Valid_Order()
        {
            await AuthanticateAccountAsync();

            var order = await CreateOrder();

            //Act
            var httpResponseMessage = _httpTestClient.DeleteAsync("/api/order/remove?Id="+order.Id).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        }


        private async Task<Order> CreateOrder()
        {
            var customer = await CreateCustomer();
            var employee = await CreateEmployee();
            var shipper = await CreateShipper();

            var orderRepository = new OrderRepository(GetContext());
            var orderModel = new Order(Guid.NewGuid(), customer, employee, DateTime.Now.AddDays(10),
                "Vins et alcools Chevalier","59 rue de l'Abbaye","Istanbul", "34000","Turkiye",shipper.Id);

            await orderRepository.Add(orderModel);
            await orderRepository.UnitOfWork.Commit();
            return orderModel;
        }

        private async Task<Customer> CreateCustomer()
        {
            var customerRepository = new CustomersRepository(GetContext());
            var customerModel = new Customer(Guid.NewGuid(), "Alfreds Futterkiste", "Maria Anders",
                "Sales Representative","sedatk1@hotmail.com", "Obere Str. 57","Istanbul", "34000", "Turkiye", "0897-034-2145");

            customerRepository.Add(customerModel);
            await customerRepository.UnitOfWork.Commit();
            return customerModel;
        }

        private async Task<Employee> CreateEmployee()
        {
            var employeeRepository = new EmployeesRepository(GetContext());
            var employyeModel = new Employee(Guid.NewGuid(),"Sedat","Kocaer", "Sales Representative",DateTime.Parse("1.05.1992 00:00:00"),
                DateTime.Parse("1.05.1992 00:00:00"), "507 - 20th Ave. E.Apt. 2A","Istanbul", "34000","Turkiye");

            await employeeRepository.Add(employyeModel);
            await employeeRepository.UnitOfWork.Commit();
            return employyeModel;
        }

        private async Task<Shipper> CreateShipper()
        {
            var shipperRepository = new ShippersRepository(GetContext());
            var shipperModel = new Shipper(Guid.NewGuid(), "SedatK Ltd", "(503) 555-9831");

            await shipperRepository.Add(shipperModel);
            await shipperRepository.UnitOfWork.Commit();
            return shipperModel;
        }
    }
}
