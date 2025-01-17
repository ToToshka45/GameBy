namespace RatingService.Application;

public sealed class RabbitMQSettings
{
    public string HostName { get; set; } = "localhost";
    public int Port { get; set; }
    public string UserName { get; set; } = "user";
    public string Password { get; set; } = "pass";
    public string VirtualHost { get; set; } = "/";

    public string UserRegisteredQueueName { get; set; } = "gamer_service_user_registered";

}
