using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Web.Application;

public class ProcessDataOutputHandler : GenericObserver<ProcessDataOutput>
{
    public override Task Update(ProcessDataOutput value)
    {
        //publish to queue
        foreach (var item in value)
        {
            Console.WriteLine(item.Key);
        }

        return Task.CompletedTask;
    }
}
