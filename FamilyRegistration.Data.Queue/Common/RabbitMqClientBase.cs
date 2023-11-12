using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace FamilyRegistration.Data.Queue.Common;

public abstract class RabbitMqClientBase : IDisposable
{
    protected IModel? Channel { get; private set; }
    private IConnection? _connection;
    private readonly ConnectionFactory _connectionFactory;
    protected readonly ILogger<RabbitMqClientBase> _logger;
    protected abstract string? QueueName { get; }

    protected RabbitMqClientBase(ConnectionFactory connectionFactory,
        ILoggerFactory loggerFactory)
    {
        _connectionFactory = connectionFactory;
        _logger = loggerFactory.CreateLogger<RabbitMqClientBase>();

        ConnectToRabbitMq();
    }

    protected virtual void Configure()
    {
        Channel?.QueueDeclare(
               queue: QueueName,
               durable: false,
               exclusive: false,
               autoDelete: false);
    }

    private void ConnectToRabbitMq()
    {
        if (_connection == null || _connection.IsOpen == false)
        {
            _connection = _connectionFactory.CreateConnection();
        }

        if (Channel == null || Channel.IsOpen == false)
        {
            Channel = _connection.CreateModel();

            Configure();
        }
    }

    public void Dispose()
    {
        try
        {
            Channel?.Close();
            Channel?.Dispose();
            Channel = null;

            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Cannot dispose RabbitMQ channel or connection");
        }
    }
}
