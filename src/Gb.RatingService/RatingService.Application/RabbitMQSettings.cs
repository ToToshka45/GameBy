namespace RatingService.Application;

public sealed class RabbitMQSettings
{
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string VirtualHost { get; set; } = default!;

    private const string _ratingServidePrefix = "rating_service_";
    public const string UserCreatedQueueName = $"{_ratingServidePrefix}user_created";
    public const string EventFinishedQueueName = $"{_ratingServidePrefix}event_finished";
}
