using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data.Queue.Common;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace FamilyRegistration.Data.Queue.BackgroundServices;

public class ProcessDataOutputProducer : DirectProducerBase<ProcessDataOutput>
{
    public ProcessDataOutputProducer(ConnectionFactory connectionFactory,
        ILoggerFactory loggerFactory)
        : base(connectionFactory, loggerFactory)
    {
    }

    protected override string AppId => nameof(ProcessDataOutputProducer);

    protected override string QueueName => "Family_Output";
}
