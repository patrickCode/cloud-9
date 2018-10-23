using Contracts;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService
{
    public class NotificationManager
    {
        private readonly EventGridClient _client;
        private readonly string _topicHostName;
        public NotificationManager(string topicEndpoint, string key)
        {
            _topicHostName = new Uri(topicEndpoint).Host;
            var topicCredentials = new TopicCredentials(key);
            _client = new EventGridClient(topicCredentials);
        }

        public async Task SendOrderNotifications(List<Event> events)
        {
            var eventGridEvents = events.Select(@event =>
            {
                return new EventGridEvent()
                {
                    Id = @event.Id,
                    EventType = @event.EventName,
                    Data = @event,
                    DataVersion = "1.0", //Should come from event
                    EventTime = DateTime.UtcNow,
                    Subject = "Order"
                };
            });

            await _client.PublishEventsAsync(_topicHostName, eventGridEvents.ToList());
        }
    }
}
