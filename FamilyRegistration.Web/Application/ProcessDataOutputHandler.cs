using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data.Queue.Common;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Web.Application;

public class ProcessDataOutputHandler : GenericObserver<ProcessDataOutput>
{
    private readonly IRabbitMqProducer<ProcessDataOutput> _producer;

    public ProcessDataOutputHandler(IRabbitMqProducer<ProcessDataOutput> producer)
    {
        _producer = producer;
    }

    public override Task Update(ProcessDataOutput value)
    {
        //publish to queue
        foreach (var item in value)
        {
            Console.WriteLine(item.Key);
        }

        //publish to queue
        _producer.Publish(value);

        return Task.CompletedTask;
    }
}
