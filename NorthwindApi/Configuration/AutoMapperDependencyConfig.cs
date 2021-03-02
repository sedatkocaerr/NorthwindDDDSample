using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NorthwindApi.Application.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Configuration
{
    public static class AutoMapperDependencyConfig
    {
        public static void AddAutoMapperDependency(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMapping), typeof(ViewModelToDomainMapping));
        }
    }
}
