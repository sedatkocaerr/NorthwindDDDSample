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
                Email = "sedatkocaer3734@hotmail.com",
                Password = "sedatkocaer"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthenticateResponse>();
            return registrationResponse.Token;
        }

        private async Task SetAccount()
        {
            var response = await _httpTestClient.PostAsJsonAsync("/api/account/Register", new AccountRegisterViewModel
            {
                Name="sedat",
                SurName="kocaer",
                Email = "sedatkocaer3734@hotmail.com",
                Password = "sedatkocaer"
            });
        }
    }
}
