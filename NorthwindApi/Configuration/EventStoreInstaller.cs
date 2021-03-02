using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Configuration
{
    public static class EventStoreInstaller
    {
        public static void CreateAndSaveEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            var eventStoreConnection = EventStoreConnection.Create(
               connectionString: configuration.GetValue<string>("EventStore:ConnectionString"),
               builder: ConnectionSettings.Create().KeepReconnecting(),
               connectionName: configuration.GetValue<string>("EventStore:ConnectionName"));

            eventStoreConnection.ConnectAsync().GetAwaiter().GetResult();

            services.AddSingleton(eventStoreConnection);
        }
    }
}
