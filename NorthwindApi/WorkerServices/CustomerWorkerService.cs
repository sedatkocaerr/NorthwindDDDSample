using AutoMapper;
using EventStore.ClientAPI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NorthwindApi.Application.ElasticSearchServices.Settings;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Events;
using NorthwindApi.Domain.Events.CustomersEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventStoreHandleService
{
    public class CustomerWorkerService : BackgroundService
    {
        private readonly ILogger<CustomerWorkerService> _logger;
        private readonly IEventStoreConnection _eventStore;
        private readonly IElasticSearchService _elasticSearchService;
        private IMapper _mapper;

        private EventStoreAllCatchUpSubscription subscription;

        public CustomerWorkerService(ILogger<CustomerWorkerService> logger, IEventStoreConnection eventStore,
            IElasticSearchService elasticSearchService, IMapper mapper)
        {
            _eventStore = eventStore;
            _elasticSearchService = elasticSearchService;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {

            var lastChechkPoint = new CheckPointEventDocument().Position;
            if (! await _elasticSearchService.CheckIndex("customerpointdata"))
            {
                await _elasticSearchService.CretaeIndex<CheckPointEventDocument>("customerpointdata", "customerpoint_history");
                lastChechkPoint = Position.Start;
                await _elasticSearchService.AddOrUpdateIndex<CheckPointEventDocument>("customerpointdata", "customerpoint_history", new CheckPointEventDocument("customersposition", lastChechkPoint.Value));
            }
            else
            {
                var response =  await _elasticSearchService.SimpleSearchAsync<CheckPointEventDocument>("customerpointdata",
                    new Nest.SearchDescriptor<CheckPointEventDocument>().Query(x => x.Term(p => p.Key, 1)));
                if(response.Documents.Count>=1)
                    lastChechkPoint = response.Documents.First().Position;

            }
            var settings = new CatchUpSubscriptionSettings(
                maxLiveQueueSize: 10000,
                readBatchSize: 500,
                verboseLogging: false,
                resolveLinkTos: false,
                subscriptionName: "customers");

            subscription = _eventStore.SubscribeToAllFrom(
                lastCheckpoint: lastChechkPoint,
                settings: settings,
                eventAppeared: async (sub, @event) =>
                {
                    if (@event.OriginalEvent.EventType.StartsWith("$"))
                        return;

                    try
                    {
                        var eventType = Type.GetType(Encoding.UTF8.GetString(@event.OriginalEvent.Metadata) + ",NorthwindApi.Domain");
                        var eventData = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(@event.OriginalEvent.Data), eventType, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                        if (eventType != typeof(CustomerAddEvent) && eventType != typeof(CustomerUpdateEvent)&& eventType != typeof(CustomerRemoveEvent))
                            return;

                        var eventSaveData = CreateViewModel(eventData);
                        if(!await _elasticSearchService.CheckIndex("customerevent"))
                        {
                            await _elasticSearchService.CretaeIndex<CustomerViewModel>("customerevent", "customerevent_history");
                        }

                        if (eventType == typeof(CustomerAddEvent) || eventType == typeof(CustomerUpdateEvent))
                        {
                            await _elasticSearchService.AddOrUpdateIndex<CustomerViewModel>("customerevent", "customerevent_history", eventSaveData);
                        }
                        else
                        {
                            await _elasticSearchService.Delete<CustomerViewModel>("customerevent", eventSaveData.Id);
                        }

                        await _elasticSearchService.AddOrUpdateIndex<CheckPointEventDocument>("customerpointdata", "customerpoint_history", new CheckPointEventDocument("customersposition", @event.OriginalPosition.GetValueOrDefault()) );
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception, exception.Message);
                    }
                },
                liveProcessingStarted: (sub) =>
                {
                    _logger.LogInformation("{SubscriptionName} subscription started.", sub.SubscriptionName);
                },
                subscriptionDropped: (sub, subDropReason, exception) =>
                {
                    _logger.LogWarning("{SubscriptionName} dropped. Reason: {SubDropReason}.", sub.SubscriptionName, subDropReason);
                });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public CustomerViewModel CreateViewModel(object @event)
        {
            switch (@event)
            {
                case CustomerAddEvent x:

                    CustomerAddEvent customerAddEvent = @event as CustomerAddEvent;
                    return _mapper.Map<CustomerViewModel>(customerAddEvent);
                case CustomerUpdateEvent x:

                    CustomerUpdateEvent customerUpdateEvent = @event as CustomerUpdateEvent;
                    return _mapper.Map<CustomerViewModel>(customerUpdateEvent);
                case CustomerRemoveEvent x:

                    CustomerRemoveEvent customerRemoveEvent = @event as CustomerRemoveEvent;
                    return _mapper.Map<CustomerViewModel>(customerRemoveEvent);
                default:
                   return  null;
            }
        }
    }
}
