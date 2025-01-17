using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RatingService.Application.Extensions;
using RatingService.Application.Models.Dtos.Users;
using RatingService.Application.Services.Abstractions;
using System.Text.Json;
namespace RatingService.Application.Services;

public sealed class MessageConsumerService : BackgroundService
{
    private readonly ILogger<MessageConsumerService> _logger;
    private readonly RabbitMQSettings _settings;
    private readonly ConnectionFactory _factory;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MessageConsumerService(IOptions<RabbitMQSettings> options, ILogger<MessageConsumerService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _settings = options.Value;
        _factory = ConnectionFactoryProvider.GetConnectionFactory(_settings);
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ReadMessages(stoppingToken);
    }

    public async Task ReadMessages(CancellationToken token)
    {
        try
        {
            using var conn = await _factory.CreateConnectionAsync(token);
            using var channel = await conn.CreateChannelAsync(cancellationToken: token);
            var scope = _serviceScopeFactory.CreateAsyncScope();
            var userLifecycleService = scope.ServiceProvider.GetRequiredService<IUserLifecycleService>();

            await channel.SetReaderConfiguration(_settings.UserRegisteredQueueName, token);
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (ch, args) =>
            {
                var userCreatedEvent = System.Text.Encoding.UTF8.GetString(args.Body.ToArray());
                // Handle the message
                _logger.LogInformation($"Received message: {userCreatedEvent}");

                var addUserDto = JsonSerializer.Deserialize<AddUserDto>(userCreatedEvent);
                ArgumentNullException.ThrowIfNull(addUserDto, nameof(addUserDto));
                await userLifecycleService.AddNewUserAsync(addUserDto, token);
                await channel.BasicAckAsync(args.DeliveryTag, false);
            };

            var consumerTag = await channel.BasicConsumeAsync(_settings.UserRegisteredQueueName, false, consumer, token);
            await channel.BasicCancelAsync(consumerTag, false, token);
            // call a AddUserService method
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogInformation(ex, "A RabbitMQ Consumer processing was canceled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while reading messages from RabbitMQ queue.");
        }
    }
}
