using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FamilyRegistration.Data.Queue.Common;

public abstract class DirectProducerBase<T> : RabbitMqClientBase, IRabbitMqProducer<T>
{
    protected abstract string AppId { get; }

    protected DirectProducerBase(ConnectionFactory connectionFactory,
        ILoggerFactory loggerFactory)
        : base(connectionFactory, loggerFactory)
    {
    }

    protected override void Configure()
    {
        base.Configure();
    }

    public virtual void Publish(T @event)
    {
        try
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
            var properties = Channel!.CreateBasicProperties();
            properties.AppId = AppId;
            properties.ContentType = "application/json";
            properties.DeliveryMode = 1; // Doesn't persist to disk
            properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            Channel.BasicPublish(exchange: string.Empty, routingKey: string.Empty, body: body, basicProperties: properties);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Error while publishing");
        }
    }
}
