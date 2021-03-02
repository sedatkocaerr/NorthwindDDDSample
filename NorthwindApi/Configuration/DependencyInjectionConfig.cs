using Microsoft.Extensions.DependencyInjection;
using NorthwindApi.CrossCutting.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            ServiceInstaller.RegisterService(services);
        }
    }
}
