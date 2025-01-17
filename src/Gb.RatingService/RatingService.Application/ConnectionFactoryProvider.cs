using RabbitMQ.Client;

namespace RatingService.Application;

public static class ConnectionFactoryProvider
{
    public static ConnectionFactory GetConnectionFactory(RabbitMQSettings settings)
    {
        return new ConnectionFactory()
        {
            HostName = settings.HostName,
            UserName = settings.UserName,
            Port = settings.Port,
            Password = settings.Password,
            VirtualHost = settings.VirtualHost,
        };
    }
}
