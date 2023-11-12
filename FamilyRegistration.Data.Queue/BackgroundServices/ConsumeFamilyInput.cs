using System.Text.Json;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Patterns.Observer;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace FamilyRegistration.Data.Queue.BackgroundServices;

public class ConsumeFamilyInput : RabbitBackgroundConsumerService
{
    private readonly IObservableOf<ProcessDataInput> _eventPublisher;

    public ConsumeFamilyInput(ILoggerFactory loggerFactory,
        ConnectionFactory connectionFactory,
        IObservableOf<ProcessDataInput> publisher)
        : base(loggerFactory, connectionFactory)
    {
        _eventPublisher = publisher;
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

        await _eventPublisher.Notify(input);

        return true;
    }


}