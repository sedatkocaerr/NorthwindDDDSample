using FluentAssertions;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Repository;
using NorthwindApi.Domain.Domain.Categories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.IntegrationTests
{
    public class CategoryControllerTest:IntegrationBase
    {
        private ICategoryRepository _categoryRepository;

        [SetUp]
        public void SetUp()
        {
            _categoryRepository = new CategoryRepository(GetContext());
        }

        [Test]
        public async Task Must_Valid_Add_Category()
        {
            await AuthanticateAccountAsync();

            var categoryViewModel = new CategoryViewModel()
            {
                Name = "necklaces",
                Description = "Explore classic and modern Tiffany necklaces and pendants, including diamond drop."
            };
            var httpResponseMessage = _httpTestClient.PostAsJsonAsync("/api/category/add", categoryViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Must_Not_Add_Valid_Category()
        {
            await AuthanticateAccountAsync();

            var employeeViewModel = new CategoryViewModel()
            {
                Name = string.Empty,
                Description = "Explore classic and modern Tiffany necklaces and pendants, including diamond drop.",
            };
            var httpResponseMessage = _httpTestClient.PostAsJsonAsync("/api/category/add", employeeViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }


        [Test]
        public async Task Must_Update_Valid_Category()
        {
            await AuthanticateAccountAsync();

            var category = await CreateCategory();

            var employeeViewModel = new CategoryViewModel()
            {
                Id= category.Id,
                Name = category.Name,
                Description = "Change Description for category.",
            };
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/category/update", employeeViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Test]
        public async Task Must_Not_Update_Valid_Category()
        {
            await AuthanticateAccountAsync();

            var category = await CreateCategory();

            var employeeViewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Name = string.Empty,
                Description = "Change Description for category.",
            };
            var httpResponseMessage = _httpTestClient.PutAsJsonAsync("/api/category/update", employeeViewModel).Result;

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        private async Task<Category> CreateCategory()
        {
            var category = new Category(Guid.NewGuid(), "Necklages", "Check out our necklaces selection.");
            await _categoryRepository.Add(category);
            await _categoryRepository.UnitOfWork.Commit();
            return category;
        }

    }
}
