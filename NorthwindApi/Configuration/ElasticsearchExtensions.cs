using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using NorthwindApi.Application.ElasticSearchServices;
using NorthwindApi.Application.ElasticSearchServices.Settings;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.ElasticSearhServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Configuration
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(
        this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IElasticSearchSettings, ElasticSearchSettings>();
            services.AddSingleton<IElasticSearchService, ElasticSearchService>();
        }
    }
}
