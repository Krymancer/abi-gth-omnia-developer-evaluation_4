using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Ambev.DeveloperEvaluation.Infrastructure.Messaging;

public sealed class EventPublisher : IAsyncDisposable
{
    private readonly ILogger<EventPublisher> _logger;
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    
    private EventPublisher(ILogger<EventPublisher> logger, IConnection connection, IChannel channel)
    {
        _logger = logger;
        _connection = connection;
        _channel = channel;
    }
    
    public static async Task<EventPublisher> CreateAsync(
        ILogger<EventPublisher> logger, 
        IOptions<RabbitMQOptions> options)
    {
        var factory = new ConnectionFactory()
        {
            HostName = options.Value.HostName,
            UserName = options.Value.UserName,
            Password = options.Value.Password
        };

        try
        {
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();
            return new EventPublisher(logger, connection, channel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating RabbitMQ connection or channel.");
            throw;
        }
    }

    public async Task EnsureQueue(string queueName)
        {
           await _channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

    public async Task PublishEvent<T>(T @event, string eventName, CancellationToken cancellationToken)
    {
        await EnsureQueue(eventName);

        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        var defaultProperties = new BasicProperties();

        await _channel.BasicPublishAsync(
            exchange: "",
            routingKey: eventName,
            mandatory: false,
            basicProperties: defaultProperties,
            body: body,
            cancellationToken: cancellationToken);

        _logger.LogInformation("{EventName} event published: {Event}", eventName, @event);
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_channel is not null)
        {
            await _channel.CloseAsync();
            await _channel.DisposeAsync();
        }
        
        if (_connection is not null)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
        }
    }
}