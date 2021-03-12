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
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace NorthwindApi.IntegrationTests
{
    public class IntegrationBase
    {
        protected readonly HttpClient _httpTestClient;
        private readonly IServiceProvider _serviceProvider;
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
                    builder.UseEnvironment("test");
                });

            _httpTestClient = appFactory.CreateClient();
            _serviceProvider = appFactory.Services;
        }

        protected async Task AuthanticateAccountAsync()
        {
            await SetAccount();
            _httpTestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }
        private async Task SetAccount()
        {
            var response = await _httpTestClient.PostAsJsonAsync("/api/account/Register", new AccountRegisterViewModel
            {
                Name = "sedat",
                SurName = "kocaer",
                Email = "sedattest@hotmail.com",
                Password = "sedatkocaer"
            });
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
        public EfDataContext GetContext()
        {
            return _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<EfDataContext>();
        }
    }
}
