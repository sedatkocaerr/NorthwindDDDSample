﻿using AutoMapper;
using EventStore.ClientAPI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NorthwindApi.Application.ElasticSearchServices.Settings;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Events.ProductsEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.WorkerServices
{
    public class ProductWorkerService : BackgroundService
    {
        private readonly ILogger<ProductWorkerService> _logger;
        private readonly IEventStoreConnection _eventStore;
        private readonly IElasticSearchService _elasticSearchService;
        private IMapper _mapper;

        private EventStoreAllCatchUpSubscription subscription;

        public ProductWorkerService(ILogger<ProductWorkerService> logger, IEventStoreConnection eventStore,
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
            if (!await _elasticSearchService.CheckIndex("productpointdata"))
            {
                await _elasticSearchService.CretaeIndex<CheckPointEventDocument>("productpointdata", "productpoint_history");
                lastChechkPoint = Position.Start;
                await _elasticSearchService.AddOrUpdateIndex<CheckPointEventDocument>("productpointdata", "productpoint_history", new CheckPointEventDocument("productposition", lastChechkPoint.Value));
            }
            else
            {
                var response = await _elasticSearchService.SimpleSearchAsync<CheckPointEventDocument>("productpointdata",
                    new Nest.SearchDescriptor<CheckPointEventDocument>().Query(x => x.Term(p => p.Key, 1)));
                if (response.Documents.Count >= 1)
                    lastChechkPoint = response.Documents.First().Position;

            }
            var settings = new CatchUpSubscriptionSettings(
                maxLiveQueueSize: 10000,
                readBatchSize: 500,
                verboseLogging: false,
                resolveLinkTos: false,
                subscriptionName: "product");

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

                        if (eventType != typeof(ProductAddEvent) && eventType != typeof(ProductUpdateEvent) && eventType != typeof(ProductRemoveEvent))
                            return;

                        var eventSaveData = CreateViewModel(eventData);
                        if (!await _elasticSearchService.CheckIndex("productevent"))
                        {
                            await _elasticSearchService.CretaeIndex<ProductViewModel>("productevent", "productevent_history");
                        }

                        if (eventType == typeof(ProductAddEvent) || eventType == typeof(ProductUpdateEvent))
                        {
                            await _elasticSearchService.AddOrUpdateIndex<ProductViewModel>("productevent", "productevent_history", eventSaveData);
                        }
                        else
                        {
                            await _elasticSearchService.Delete<OrderViewModel>("productevent", eventSaveData.Id);
                        }
                      

                        await _elasticSearchService.AddOrUpdateIndex<CheckPointEventDocument>("productpointdata", "productpoint_history", new CheckPointEventDocument("productposition", @event.OriginalPosition.GetValueOrDefault()));
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

        public ProductViewModel CreateViewModel(object @event)
        {
            switch (@event)
            {
                case ProductAddEvent x:

                    ProductAddEvent productAddEvent = @event as ProductAddEvent;
                    return _mapper.Map<ProductViewModel>(productAddEvent);
                case ProductUpdateEvent x:

                    ProductUpdateEvent productUpdateEvent = @event as ProductUpdateEvent;
                    return _mapper.Map<ProductViewModel>(productUpdateEvent);
                case ProductRemoveEvent x:

                    ProductRemoveEvent productRemoveEvent = @event as ProductRemoveEvent;
                    return _mapper.Map<ProductViewModel>(productRemoveEvent);
                default:
                    return null;
            }
        }
    }
}
