using System.Text.Json;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Patterns.Observer;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace FamilyRegistration.Data.Queue.BackgroundServices;

public class ConsumeFamilyInput : RabbitBackgroundConsumerService
{
    private readonly ISubject<ProcessDataInput> _publisher;

    public ConsumeFamilyInput(ILoggerFactory loggerFactory, ConnectionFactory connectionFactory, ISubject<ProcessDataInput> publisher)
        : base(loggerFactory, connectionFactory)
    {
        _publisher = publisher;
    }

    protected override string QueueName => "Family_Input";

    public override async Task<bool> HandleMessage(string? content)
    {
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var jsonData = JsonSerializer.Deserialize<JsonFormatOne[]>(content!, serializeOptions);

        var input = jsonData!.Select(s => s.Adapt()).AsInput();

        await _publisher.Publish(input);

        return true;
    }


}