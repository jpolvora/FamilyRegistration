using System.Text;
using System.Text.Json;
using FamilyRegistration.Core.Decorator;
using FamilyRegistration.Core.Strategy;
using FamilyRegistration.Core.UseCases.ProcessData;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FamilyRegistration.Data.Queue;

public class ConsumeRabbitMQHostedService : BackgroundService
{
    private readonly ILogger _logger;
    private IConnection? _connection;
    private IModel? _channel;

    public ConsumeRabbitMQHostedService(ILoggerFactory loggerFactory, AmqpSettings settings)
    {
        _logger = loggerFactory.CreateLogger<ConsumeRabbitMQHostedService>();
        InitRabbitMQ(settings);
    }

    private void InitRabbitMQ(AmqpSettings settings)
    {
        var factory = new ConnectionFactory()
        {
            HostName = settings.HostName,
            UserName = settings.UserName,
            Password = settings.Password,
            VirtualHost = settings.VirtualHost
        };

        // create connection  
        _connection = factory.CreateConnection();

        // create channel  
        _channel = _connection.CreateModel();

        //_channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
        _channel.QueueDeclare("Family_Input", false, false, false, null);
        //_channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
        _channel.BasicQos(0, 1, false);

        //_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
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

        _channel.BasicConsume("Family_Input", true, consumer);
        return Task.CompletedTask;
    }

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
        _logger.LogInformation("OnConsumerShutdown");
    }

    private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
    {
        var content = Encoding.UTF8.GetString(e.Body.ToArray());

        // handle the received message  
        await HandleMessage(content);
        //if (result == true) _channel?.BasicAck(e.DeliveryTag, false);
        //_channel?.BasicNack(e.DeliveryTag, false, false);
    }

    public async Task<bool> HandleMessage(string? content)
    {
        if (content == null) return false;

        try
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var jsonData = JsonSerializer.Deserialize<JsonFormatOne[]>(content, serializeOptions)
                ?? throw new Exception("Empty jsonData after deserializing");

            var input = jsonData.Select(s => s.Adapt()).AsInput();

            //instanciar useCase e executar
            //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline
            IProcessDataStrategy strategy = new ProcessDataWithDecorator(new AggregateScoreCalculator());
            IProcessDataUseCase useCase = new ProcessDataUseCase(strategy);
            var output = await useCase.Execute(input);
            //ordenar o output pelo Score mais alto
            var result = new ProcessDataOutput(output.OrderByDescending(x => x.Score));
            var textToLog = result.ToString();
            _logger.LogInformation("Message to Log {Log}", textToLog);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(message: ex.ToString());
        }

        return false;
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}
