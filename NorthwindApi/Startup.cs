using EventStoreHandleService;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NorthwindApi.Configuration;
using NorthwindApi.CrossCutting.IoC;
using NorthwindApi.Domain.Domain.Customers;
using NorthwindApi.NorthwindApi;
using NorthwindApi.WorkerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(opt=>opt.SerializerSettings.ReferenceLoopHandling=ReferenceLoopHandling.Ignore);

            // Db Settings
            services.RegisterDb(Configuration);

            // AutoMapper Settings
            services.AddAutoMapperDependency();

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // Media Store Installer
            services.CreateAndSaveEventStore(Configuration);

            //Elastic Search COnfiguraiton 
            services.AddElasticsearch(Configuration);

            // Services , Repository , etc dependency
            services.AddDependencyInjectionConfiguration();

            // Add Hosted Service Dependency
            services.AddHostedService<CustomerWorkerService>();
            services.AddHostedService<EmployeeWorkerService>();
            services.AddHostedService<OrderWorkerService>();
            services.AddHostedService<ProductWorkerService>();
            services.AddHostedService<SupplierWorkerService>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            
            // closed for Docker
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
