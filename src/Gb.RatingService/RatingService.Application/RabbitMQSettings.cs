namespace RatingService.Application;

public sealed class RabbitMQSettings
{
    public string HostName { get; set; } = "localhost";
    public int Port { get; set; }
    public string UserName { get; set; } = "user";
    public string Password { get; set; } = "pass";
    public string VirtualHost { get; set; } = "/";

    public const string UserCreatedQueueName = "gamer_service_user_created";
    public const string EventFinishedQueueName = "event_service_event_finished";
}
