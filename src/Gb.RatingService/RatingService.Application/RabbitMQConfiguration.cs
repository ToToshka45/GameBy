using System.Collections;

namespace RatingService.Application
{
    public sealed class RabbitMQConfigurations : IEnumerable<ConfigurationDetails>
    {
        public IEnumerable<ConfigurationDetails> ConfigurationDetails { get; set; } = [];

        public IEnumerator<ConfigurationDetails> GetEnumerator()
        {
            return ConfigurationDetails.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string? UserCreatedQueueName => ConfigurationDetails.FirstOrDefault()?.Queues.FirstOrDefault(q => q.Name.Contains("user_created"))?.Name;
        public string? EventFinishedQueueName => ConfigurationDetails.FirstOrDefault()?.Queues.FirstOrDefault(q => q.Name.Contains("event_finished"))?.Name;
    }

    public sealed record ConfigurationDetails(string ExchangeName, string ExchangeType, IEnumerable<RabbitQueue> Queues);
    public sealed record RabbitQueue(string Name, string RoutingKey);
}
