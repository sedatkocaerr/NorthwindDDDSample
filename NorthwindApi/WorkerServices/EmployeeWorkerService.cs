using AutoMapper;
using EventStore.ClientAPI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NorthwindApi.Application.ElasticSearchServices.Settings;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Events.EmployeesEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindApi.WorkerServices
{
    public class EmployeeWorkerService : BackgroundService
    {
        private readonly ILogger<EmployeeWorkerService> _logger;
        private readonly IEventStoreConnection _eventStore;
        private readonly IElasticSearchService _elasticSearchService;
        private IMapper _mapper;

        private EventStoreAllCatchUpSubscription subscription;

        public EmployeeWorkerService(ILogger<EmployeeWorkerService> logger, IEventStoreConnection eventStore,
            IElasticSearchService elasticSearchService, IMapper mapper)
        {
            _eventStore = eventStore;
            _elasticSearchService = elasticSearchService;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {

            if (!await _elasticSearchService.CheckIndex(ElasticSearchIndexDocumentNames.EmployeeIndexName))
            {
                await _elasticSearchService.CretaeIndex<ProductViewModel>(ElasticSearchIndexDocumentNames.EmployeeIndexName,
                    ElasticSearchIndexDocumentNames.EmployeeIndexAliasName);
            }
            var lastCheckpoint = await new CreateAndUpdatePointDocumentIndex(_elasticSearchService).addPointDocument(
                 ElasticSearchIndexDocumentNames.EmployeeDocumentPositionIndexName,
                 ElasticSearchIndexDocumentNames.EmployeeDocumentPositionAliasName,
                 ElasticSearchIndexDocumentNames.EmployeeDocumentPositionName);

            var settings = new CatchUpSubscriptionSettings(
                maxLiveQueueSize: 10000,
                readBatchSize: 500,
                verboseLogging: false,
                resolveLinkTos: false,
                subscriptionName: "employee");

            subscription = _eventStore.SubscribeToAllFrom(
                lastCheckpoint: lastCheckpoint,
                settings: settings,
                eventAppeared: async (sub, @event) =>
                {
                    if (@event.OriginalEvent.EventType.StartsWith("$"))
                        return;

                    try
                    {
                        var eventType = Type.GetType(Encoding.UTF8.GetString(@event.OriginalEvent.Metadata) + ",NorthwindApi.Domain");
                        var eventData = Newtonsoft.Json.JsonConvert.DeserializeObject(Encoding.UTF8.GetString(@event.OriginalEvent.Data), eventType);

                        if (eventType != typeof(EmployeeAddEvent) && eventType != typeof(EmployeeUpdateEvent) && eventType != typeof(EmployeeRemoveEvent))
                            return;

                        var eventSaveData = CreateViewModel(eventData);
                      
                        if (eventType == typeof(EmployeeAddEvent) || eventType == typeof(EmployeeUpdateEvent))
                        {
                            await _elasticSearchService.AddOrUpdateIndex<EmployeeViewModel>(ElasticSearchIndexDocumentNames.EmployeeIndexName,
                               ElasticSearchIndexDocumentNames.EmployeeIndexAliasName, eventSaveData);
                        }
                        else
                        {
                            await _elasticSearchService.Delete<EmployeeViewModel>(ElasticSearchIndexDocumentNames.EmployeeIndexName, eventSaveData.Id);
                        }

                        await new CreateAndUpdatePointDocumentIndex(_elasticSearchService).UpdatePointDocument(
                        ElasticSearchIndexDocumentNames.EmployeeDocumentPositionIndexName,
                        ElasticSearchIndexDocumentNames.EmployeeDocumentPositionAliasName,
                        ElasticSearchIndexDocumentNames.EmployeeDocumentPositionName, @event.OriginalPosition.GetValueOrDefault());
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

        public EmployeeViewModel CreateViewModel(object @event)
        {
            switch (@event)
            {
                case EmployeeAddEvent x:

                    EmployeeAddEvent employeeAddEvent = @event as EmployeeAddEvent;
                    return _mapper.Map<EmployeeViewModel>(employeeAddEvent);
                case EmployeeUpdateEvent x:

                    EmployeeUpdateEvent employeeUpdateEvent = @event as EmployeeUpdateEvent;
                    return _mapper.Map<EmployeeViewModel>(employeeUpdateEvent);
                case EmployeeRemoveEvent x:

                    EmployeeRemoveEvent employeeRemoveEvent = @event as EmployeeRemoveEvent;
                    return _mapper.Map<EmployeeViewModel>(employeeRemoveEvent);
                default:
                    return null;
            }
        }
    }
}
