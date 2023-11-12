using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data.Queue.Common;
using RabbitMQ.Client;

namespace FamilyRegistration.Web.Application;

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
