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

        [Test]
        public async Task Must_Not_Add_Valid_Employee()
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
                City = string.Empty,
                PostalCode = "98122",
                Country = "USA"
            };
            var httpResponseMessage = _httpTestClient.PostAsJsonAsync("/api/employee/add", employeeViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }


        [Test]
        public async Task Must_Update_Valid_Employee()
        {
            await AuthanticateAccountAsync();

            var employee = await CreateEmployee();

            var employeeViewModel = new EmployeeViewModel()
            {
                Id=employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Title = employee.Title,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = "Tr",
                PostalCode = employee.PostalCode,
                Country = employee.Country
            };

            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/employee/update", employeeViewModel).Result;
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedEmployee = await _employeeRepository.FindById(employeeViewModel.Id);

            updatedEmployee.City.Should().Be(employeeViewModel.City);

        }

        [Test]
        public async Task Must_NOt_Update_Valid_Employee()
        {
            await AuthanticateAccountAsync();

            var employee = await CreateEmployee();

            var employeeViewModel = new EmployeeViewModel()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Title = employee.Title,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = string.Empty,
                City = employee.City,
                PostalCode = employee.PostalCode,
                Country = employee.Country
            };

            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/employee/update", employeeViewModel).Result;
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        [Test]
        public async Task Must_Remove_Valid_Employee()
        {
            await AuthanticateAccountAsync();

            var employee = await CreateEmployee();

            //Act
            var httpResponseMessage = _httpTestClient.DeleteAsync("/api/employee/remove?Id=" + employee.Id).Result;

            //Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        public async Task<Employee> CreateEmployee()
        {
            var employee = new Employee(
                Guid.NewGuid(),"Davolio","Nancy","Sales Representative",DateTime.Parse("8.12.1948 00:00:00"),
                DateTime.Parse("1.05.1992 00:00:00"), "507 - 20th Ave. E.Apt. 2A", "Seattle", "98122","USA"
            );
            await _employeeRepository.Add(employee);
            await _employeeRepository.UnitOfWork.Commit();
            return employee;
        }
    }
}
