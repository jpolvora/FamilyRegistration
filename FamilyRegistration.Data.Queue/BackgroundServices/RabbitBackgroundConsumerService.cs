using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FamilyRegistration.Data.Queue.BackgroundServices;

public abstract class RabbitBackgroundConsumerService : BackgroundService
{
    protected abstract string QueueName { get; }

    private readonly ILogger _logger;
    private readonly ConnectionFactory _connectionFactory;
    private IConnection? _connection;
    private IModel? _channel;

    public RabbitBackgroundConsumerService(ILoggerFactory loggerFactory, ConnectionFactory connectionFactory)
    {
        _logger = loggerFactory.CreateLogger<RabbitBackgroundConsumerService>();
        _connectionFactory = connectionFactory;
        InitRabbitMQ();
    }

    private void InitRabbitMQ()
    {
        // create connection  
        _connection = _connectionFactory.CreateConnection();

        // create channel  
        _channel = _connection.CreateModel();

        //_channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
        _channel.QueueDeclare(QueueName, false, false, false, null);
        //_channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
        _channel.BasicQos(0, 1, false);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += Consumer_Received;
        consumer.Shutdown += OnConsumerShutdown;
        consumer.Registered += OnConsumerRegistered;
        consumer.Unregistered += OnConsumerUnregistered;
        consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

        _channel.BasicConsume(QueueName, false, consumer);

        return Task.CompletedTask;
    }

    private async void Consumer_Received(object? sender, BasicDeliverEventArgs @event)
    {
        var content = Encoding.UTF8.GetString(@event.Body.ToArray());
        var success = false;
        try
        {
            success = await HandleMessage(content);
        }
        catch (Exception ex)
        {
            success = false;
            _logger.LogError("Error handling message", ex);
        }
        finally
        {
            if (success) _channel?.BasicAck(@event.DeliveryTag, false);
            else _channel?.BasicNack(@event.DeliveryTag, false, false);

            _logger.LogInformation("Message Consumed {success}", success);
        }
    }

    public abstract Task<bool> HandleMessage(string? content);


    private void OnConsumerConsumerCancelled(object? sender, ConsumerEventArgs e)
    {
        _logger.LogInformation("OnConsumerConsumerCancelled");
    }

    private void OnConsumerUnregistered(object? sender, ConsumerEventArgs e)
    {
        _logger.LogInformation("OnConsumerUnregistered");
    }

    private void OnConsumerRegistered(object? sender, ConsumerEventArgs e)
    {
        _logger.LogInformation("OnConsumerRegistered");
    }

    private void OnConsumerShutdown(object? sender, ShutdownEventArgs e)
    {
        var consumer = sender as EventingBasicConsumer;
        if (consumer is not null)
        {
            consumer.Received -= Consumer_Received;
        }

        _logger.LogInformation("OnConsumerShutdown");
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}
