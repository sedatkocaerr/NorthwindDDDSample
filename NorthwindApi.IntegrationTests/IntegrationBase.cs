using Microsoft.AspNetCore.Mvc.Testing;
using NorthwindApi.Data.Ef;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NorthwindApi.Application.Authentication.Request;
using System.Net.Http.Headers;
using NorthwindApi.Application.Authentication.Response;
using NorthwindApi.Application.ViewModels;
using System;
using NorthwindApi.Domain.Core.Command;

namespace NorthwindApi.IntegrationTests
{
    public class IntegrationBase
    {
        protected readonly HttpClient _httpTestClient;

        protected IntegrationBase()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(EfDataContext));
                        services.AddDbContext<EfDataContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });

            _httpTestClient = appFactory.CreateClient();
        }

        protected async Task AuthanticateAccountAsync()
        {
            await SetAccount();
            _httpTestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await _httpTestClient.PostAsJsonAsync("/api/account/login", new AuthenticateRequest
            {
                Email = "sedattest@hotmail.com",
                Password = "sedatkocaer"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthenticateResponse>();
            return registrationResponse.Token;
        }

        protected async Task<Guid> CreateCategory()
        {
            var response = await _httpTestClient.PostAsJsonAsync("/api/category/add", new CategoryViewModel
            {
               Name="Necklages",
               Description= "Check out our necklaces selection."
            });
            var categoryResponse = await response.Content.ReadAsAsync<CommandResponse>();
            return categoryResponse.Id;
        }

        protected async Task<Guid> CreateSupplier()
        {
            var response = await _httpTestClient.PostAsJsonAsync("/api/supplier/add", new SupplierViewModel
            {
                CompanyName= "Bulk Supply",
                ContactName= "Sedat Kocaer",
                ContactTitle= "Purchasing Manager",
                Adress= "49 Gilbert St.",
                City="Istanbul",
                Country="TR",
                Phone= "(171) 555-2222"
            });
            var supplierResponse = await response.Content.ReadAsAsync<CommandResponse>();
            return supplierResponse.Id;
        }

        protected async Task<ProductViewModel> CreateProduct()
        {
            var productViewModel = new ProductViewModel
            {
                CategoryID = await CreateCategory(),
                SupplierID = await CreateSupplier(),
                ProductName = "3MM Rope Chain Necklace",
                QuantityPerUnit = "1",
                UnitPrice = 10,
                UnitsInStock = 10
            };
            var response = await _httpTestClient.PostAsJsonAsync("/api/product/add", productViewModel);
            var productResponse = await response.Content.ReadAsAsync<CommandResponse>();

            productViewModel.Id = productResponse.Id;
            return productViewModel;
        }

        private async Task SetAccount()
        {
            var response = await _httpTestClient.PostAsJsonAsync("/api/account/Register", new AccountRegisterViewModel
            {
                Name="sedat",
                SurName="kocaer",
                Email = "sedattest@hotmail.com",
                Password = "sedatkocaer"
            });
        }
    }
}
