using AutoMapper;
using EventStore.ClientAPI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NorthwindApi.Application.ElasticSearchServices.Settings;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Events.SupplierEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.WorkerServices
{
    public class SupplierWorkerService : BackgroundService
    {
        private readonly ILogger<SupplierWorkerService> _logger;
        private readonly IEventStoreConnection _eventStore;
        private readonly IElasticSearchService _elasticSearchService;
        private IMapper _mapper;

        private EventStoreAllCatchUpSubscription subscription;

        public SupplierWorkerService(ILogger<SupplierWorkerService> logger, IEventStoreConnection eventStore,
            IElasticSearchService elasticSearchService, IMapper mapper)
        {
            _logger = logger;
            _eventStore = eventStore; 
             _elasticSearchService = elasticSearchService;
            _mapper = mapper;
        }

        public async override Task StartAsync(CancellationToken cancellationToken)
        {
            var lastChechkPoint = new CheckPointEventDocument().Position;
            if (!await _elasticSearchService.CheckIndex("supplierpointdata"))
            {
                await _elasticSearchService.CretaeIndex<CheckPointEventDocument>("supplierpointdata", "supplierpoint_history");
                lastChechkPoint = Position.Start;
                await _elasticSearchService.AddOrUpdateIndex<CheckPointEventDocument>("supplierpointdata", "supplierpoint_history", new CheckPointEventDocument("supplierposition", lastChechkPoint.Value));
            }
            else
            {
                var response = await _elasticSearchService.SimpleSearchAsync<CheckPointEventDocument>("supplierpointdata",
                    new Nest.SearchDescriptor<CheckPointEventDocument>().Query(x => x.Term(p => p.Key, 1)));
                if (response.Documents.Count >= 1)
                    lastChechkPoint = response.Documents.First().Position;

            }
            var settings = new CatchUpSubscriptionSettings(
                maxLiveQueueSize: 10000,
                readBatchSize: 500,
                verboseLogging: false,
                resolveLinkTos: false,
                subscriptionName: "supplier");

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
                        var eventData = Newtonsoft.Json.JsonConvert.DeserializeObject(Encoding.UTF8.GetString(@event.OriginalEvent.Data), eventType);

                        if (eventType != typeof(SupplierAddEvent) && eventType != typeof(SupplierUpdateEvent) && eventType != typeof(SupplierRemoveEvent))
                            return;

                        var eventSaveData = CreateViewModel(eventData);
                        if (!await _elasticSearchService.CheckIndex("supplierevent"))
                        {
                            await _elasticSearchService.CretaeIndex<SupplierViewModel>("supplierevent", "supplierevent_history");
                        }

                        if (eventType == typeof(SupplierAddEvent) || eventType == typeof(SupplierUpdateEvent))
                        {
                            await _elasticSearchService.AddOrUpdateIndex<SupplierViewModel>("supplierevent", "supplierevent_history", eventSaveData);
                        }
                        else
                        {
                            await _elasticSearchService.Delete<OrderViewModel>("supplierevent", eventSaveData.Id);
                        }

                        await _elasticSearchService.AddOrUpdateIndex<CheckPointEventDocument>("supplierpointdata", "supplierpoint_history", new CheckPointEventDocument("supplierposition", @event.OriginalPosition.GetValueOrDefault()));
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

        public SupplierViewModel CreateViewModel(object @event)
        {
            switch (@event)
            {
                case SupplierAddEvent x:
                    SupplierAddEvent supplierAddEvent = @event as SupplierAddEvent;
                    return _mapper.Map<SupplierViewModel>(supplierAddEvent);

                case SupplierUpdateEvent x:
                    SupplierUpdateEvent supplierUpdateEvent = @event as SupplierUpdateEvent;
                    return _mapper.Map<SupplierViewModel>(supplierUpdateEvent);

                case SupplierRemoveEvent x:
                    SupplierRemoveEvent supplierRemoveEvent = @event as SupplierRemoveEvent;
                    return _mapper.Map<SupplierViewModel>(supplierRemoveEvent);

                default:
                    return null;
            }
        }
    }
}
