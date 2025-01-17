using RabbitMQ.Client;
namespace RatingService.Application.Extensions;

internal static class RabbitMQExtensions
{
    internal static async Task<IChannel> SetReaderConfiguration(this IChannel channel, string queue, CancellationToken token)
    {
        await channel.QueueDeclareAsync(queue, cancellationToken: token);
        await channel.BasicQosAsync(0, 3, false, cancellationToken: token);
        return channel;
    }
    
    internal static async Task<IChannel> SetWriterConfiguration(this IChannel channel, string queue, CancellationToken token)
    {
        await channel.QueueDeclareAsync(queue, cancellationToken: token);
        await channel.BasicQosAsync(0, 3, false, cancellationToken: token);
        return channel;
    }
}
