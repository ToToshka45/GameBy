﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RatingService.Application.Models.Dtos.Participants;
using RatingService.Application.Models.Dtos.Users;
using RatingService.Application.Services.Abstractions;
using RatingService.Application.Services.Interfaces;
using System.Text.Json;
namespace RatingService.Application.Services;

public sealed class EventFinishedRabbitEventConsumer : IBaseEventConsumer
{
    private readonly ILogger<UserCreatedRabbitEventConsumer> _logger;
    private readonly RabbitMQSettings _settings;
    private readonly ConnectionFactory _factory;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private IConnection? _conn;
    private IChannel? _channel;
    private string? _consumerTag;

    const string CURRENT_QUEUE = RabbitMQSettings.EventFinishedQueueName;

    public EventFinishedRabbitEventConsumer(IOptions<RabbitMQSettings> options, ILogger<UserCreatedRabbitEventConsumer> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _settings = options.Value;
        _factory = ConnectionFactoryProvider.GetConnectionFactory(_settings);
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _conn = await _factory.CreateConnectionAsync(cancellationToken);
        _channel = await _conn.CreateChannelAsync(cancellationToken: cancellationToken);
        await ReadMessages(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null)
        {
            if (!string.IsNullOrEmpty(_consumerTag))
                await _channel.BasicCancelAsync(_consumerTag, cancellationToken: cancellationToken);

            await _channel.CloseAsync();
        }
        if (_conn != null)
            await _conn.CloseAsync();
    }

    public async Task ReadMessages(CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(_channel);
        try
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            var handler = scope.ServiceProvider.GetRequiredService<IEventLifecycleService>();
            await _channel.QueueDeclareAsync(CURRENT_QUEUE, exclusive: false, autoDelete: false, cancellationToken: token);
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (ch, args) =>
            {
                try
                {
                    var userCreatedEvent = System.Text.Encoding.UTF8.GetString(args.Body.ToArray());
                    // Handle the message
                    _logger.LogInformation($"Received message: {userCreatedEvent}");

                    var dto = JsonSerializer.Deserialize<FinalizeEventDto>(userCreatedEvent);
                    ArgumentNullException.ThrowIfNull(dto, nameof(dto));
                    await handler.FinalizeEventAsync(dto, token);
                    await _channel.BasicAckAsync(args.DeliveryTag, false, token);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occured while reading and processing the queue '{CURRENT_QUEUE}' messages");
                }
            };

            _consumerTag = await _channel.BasicConsumeAsync(CURRENT_QUEUE, false, consumer, token);
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

    public async ValueTask DisposeAsync()
    {
        if (_channel != null)
            await _channel.DisposeAsync();
        if (_conn != null)
            await _conn.DisposeAsync();
    }
}
