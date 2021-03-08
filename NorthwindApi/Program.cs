using EventStoreHandleService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NorthwindApi.Data.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            CreateHostBuilder(args).Build().Run();

            //using var scope = host.Services.CreateScope();
            //var services = scope.ServiceProvider;

            //try
            //{
            //    var dbContext = services.GetRequiredService<EfDataContext>();
            //    if (dbContext.Database.IsSqlServer())
            //    {
            //        dbContext.Database.Migrate();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            //    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

            //    throw;
            //}

             //host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
