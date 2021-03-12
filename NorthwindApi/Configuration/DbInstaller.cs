using Microsoft.Extensions.DependencyInjection;
using NorthwindApi.Data.Ef;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NorthwindApi.NorthwindApi
{
    public static class DbInstaller
    {
        public static void RegisterDb(this IServiceCollection services,
            IConfiguration configuration,bool testdb)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if(testdb)
            {
                services.AddDbContext<EfDataContext>(options => options.UseInMemoryDatabase("test"));
            }
            else
            {
                services.AddDbContext<EfDataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(EfDataContext).Assembly.FullName)));
            }
            
        }
    }
}
