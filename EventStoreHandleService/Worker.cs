using EventStore.ClientAPI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventStoreHandleService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEventStoreConnection _eventStore;
        private readonly CheckpointRepository _checkpointRepository;
        private readonly TaskRepository _taskRepository;

        private EventStoreAllCatchUpSubscription subscription;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var settings = new CatchUpSubscriptionSettings(
                maxLiveQueueSize: 10000,
                readBatchSize: 500,
                verboseLogging: false,
                resolveLinkTos: false,
                subscriptionName: "Tasks");

            subscription = _eventStore.SubscribeToAllFrom(
                lastCheckpoint: lastCheckpoint,
                settings: settings,
                eventAppeared: async (sub, @event) =>
                {
                    if (@event.OriginalEvent.EventType.StartsWith("$"))
                        return;

                    try
                    {
                        var eventType = Type.GetType(Encoding.UTF8.GetString(@event.OriginalEvent.Metadata));
                        var eventData = JsonSerializer.Deserialize(Encoding.UTF8.GetString(@event.OriginalEvent.Data), eventType);

                        if (eventType != typeof(CreatedTask) && eventType != typeof(AssignedTask) && eventType != typeof(MovedTask) && eventType != typeof(CompletedTask))
                            return;

                        _taskRepository.Save(eventData);

                        await _checkpointRepository.SaveAsync("tasks", @event.OriginalPosition.GetValueOrDefault());
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
    }
}
