using EventStore.ClientAPI;
using MediatR;
using Newtonsoft.Json;
using NorthwindApi.Domain;
using NorthwindApi.Domain.Core;
using NorthwindApi.Domain.Core.Domain;
using NorthwindApi.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NorthwindApi.Data.Repository.EventSource
{
    public class EventSourceRepository : IEventSourceRepository
    {
        private readonly IEventStoreConnection _eventStore;
        public EventSourceRepository(IEventStoreConnection eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task SaveAsync<T>(Entity aggregate) where T : Entity
        {
            var events = aggregate.GetChanges().Select(@event=>new EventData(Guid.NewGuid(),
                    @event.GetType().Name,
                    true,
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)),
                    Encoding.UTF8.GetBytes(@event.GetType().FullName)));

            if (!events.Any())
            {
                return;
            }

            var streamName = GetStreamName(aggregate, aggregate.Id);
            var result = await _eventStore.AppendToStreamAsync(streamName, ExpectedVersion.Any, events);
        }

        public async Task<T> LoadAsync<T>(Guid aggregateId) where T : Entity,new ()
        {
            if (aggregateId == Guid.Empty)
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(aggregateId));

            var aggregate = new T();
            var streamName = GetStreamName(aggregate, aggregateId);

            var nextPageStart = 0L;

            do
            {
                var page = await _eventStore.ReadStreamEventsForwardAsync(
                    streamName, nextPageStart, 4096, false);

                if (page.Events.Length > 0)
                {
                    aggregate.Load(
                        page.Events.Last().Event.EventNumber,
                        page.Events.Select(@event => JsonConvert.DeserializeObject(Encoding.UTF8.GetString(@event.OriginalEvent.Data), Type.GetType(Encoding.UTF8.GetString(@event.OriginalEvent.Metadata)+ ",NorthwindApi.Domain"))
                        ).ToArray());
                }

                nextPageStart = !page.IsEndOfStream ? page.NextEventNumber : -1;
            } while (nextPageStart != -1);

            return aggregate;
        }

        

        public void Dispose()
        {
            // TODO implement dispose
            return;
        }

        private string GetStreamName<T>(T type, Guid aggregateId) => $"{type.GetType().Name}-{aggregateId}";
    }
}
