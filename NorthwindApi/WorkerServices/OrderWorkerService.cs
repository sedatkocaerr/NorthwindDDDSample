using AutoMapper;
using EventStore.ClientAPI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NorthwindApi.Application.ElasticSearchServices.Settings;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Events.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.WorkerServices
{
    public class OrderWorkerService : BackgroundService
    {
        private readonly ILogger<OrderWorkerService> _logger;
        private readonly IEventStoreConnection _eventStore;
        private readonly IElasticSearchService _elasticSearchService;
        private IMapper _mapper;

        private EventStoreAllCatchUpSubscription subscription;

        public OrderWorkerService(ILogger<OrderWorkerService> logger, IEventStoreConnection eventStore, IElasticSearchService elasticSearchService, IMapper mapper)
        {
            _logger = logger;
            _eventStore = eventStore;
            _elasticSearchService = elasticSearchService;
            _mapper = mapper;
        }

        public async override Task StartAsync(CancellationToken cancellationToken)
        {
            var lastChechkPoint = new CheckPointEventDocument().Position;
            if (!await _elasticSearchService.CheckIndex("orderpointdata"))
            {
                await _elasticSearchService.CretaeIndex<CheckPointEventDocument>("orderpointdata", "orderpoint_history");
                lastChechkPoint = Position.Start;
                await _elasticSearchService.AddOrUpdateIndex<CheckPointEventDocument>("orderpointdata", "orderpoint_history", new CheckPointEventDocument("orderposition", lastChechkPoint.Value));
            }
            else
            {
                var response = await _elasticSearchService.SimpleSearchAsync<CheckPointEventDocument>("orderpointdata",
                    new Nest.SearchDescriptor<CheckPointEventDocument>().Query(x => x.Term(p => p.Key, 1)));
                if (response.Documents.Count >= 1)
                    lastChechkPoint = response.Documents.First().Position;

            }
            var settings = new CatchUpSubscriptionSettings(
                maxLiveQueueSize: 10000,
                readBatchSize: 500,
                verboseLogging: false,
                resolveLinkTos: false,
                subscriptionName: "order");

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

                        if (eventType != typeof(OrderAddEvent) && eventType != typeof(OrderUpdateEvent) && eventType != typeof(OrderRemoveEvent))
                            return;

                        var eventSaveData = CreateViewModel(eventData);
                        if (!await _elasticSearchService.CheckIndex("orderevent"))
                        {
                            await _elasticSearchService.CretaeIndex<OrderViewModel>("orderevent", "orderevent_history");
                        }

                        if (eventType == typeof(OrderAddEvent) || eventType == typeof(OrderUpdateEvent))
                        {
                            await _elasticSearchService.AddOrUpdateIndex<OrderViewModel>("orderevent", "orderevent_history", eventSaveData);
                        }
                        else
                        {
                            await _elasticSearchService.Delete<OrderViewModel>("orderevent", eventSaveData.Id);
                        }
                        

                        await _elasticSearchService.AddOrUpdateIndex<CheckPointEventDocument>("orderpointdata", "orderpoint_history", new CheckPointEventDocument("orderposition", @event.OriginalPosition.GetValueOrDefault()));
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


        public OrderViewModel CreateViewModel(object @event)
        {
            switch (@event)
            {
                case OrderAddEvent x:

                    OrderAddEvent orderAddEvent = @event as OrderAddEvent;
                    return _mapper.Map<OrderViewModel>(orderAddEvent);
                case OrderUpdateEvent x:

                    OrderUpdateEvent orderUpdateEvent = @event as OrderUpdateEvent;
                    return _mapper.Map<OrderViewModel>(orderUpdateEvent);
                case OrderRemoveEvent x:

                    OrderRemoveEvent orderRemoveEvent = @event as OrderRemoveEvent;
                    return _mapper.Map<OrderViewModel>(orderRemoveEvent);
                default:
                    return null;
            }
        }
    }
}
