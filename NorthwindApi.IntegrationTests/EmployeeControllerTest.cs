using FluentAssertions;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Repository;
using NorthwindApi.Domain.Domain.Employees;
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
    public class EmployeeControllerTest:IntegrationBase
    {
        private IEmployeesRepository _employeeRepository;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = new EmployeesRepository(GetContext());
        }


        [Test]
        public async Task Must_Add_Valid_Employee()
        {
            await AuthanticateAccountAsync();

            var employeeViewModel = new EmployeeViewModel()
            {
                FirstName = "Davolio",
                LastName = "Nancy",
                Title = "Sales Representative",
                BirthDate = DateTime.Parse("8.12.1948 00:00:00"),
                HireDate = DateTime.Parse("1.05.1992 00:00:00"),
                Address = "507 - 20th Ave. E.Apt. 2A",
                City = "Seattle",
                PostalCode = "98122",
                Country = "USA"
            };
            var httpResponseMessage = _httpTestClient.PostAsJsonAsync("/api/employee/add", employeeViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        }
    }
}
