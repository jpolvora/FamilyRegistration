using FamilyRegistration.Core.Observer;
using FamilyRegistration.Core.Observer.Observers;
using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Strategy;

public class ProcessDataWithObservers : IProcessDataStrategy
{
    public async Task<ProcessDataOutput> Execute(ProcessDataInput input)
    {
        var output = new ProcessDataOutput();

        using var publisher = new FamilyRegistrationContextPublisher();
        publisher.Register(new NumOfDependentsObserver());
        publisher.Register(new FamilyIncomeObserver());

        foreach (var inputItem in input)
        {
            var context = inputItem.AdaptToFamilyRegistrationContext();

            //will trigger updates in all observers asynchronously
            await publisher.Notify(context);

            var outputItem = context.AdaptToOutputItem();
            output.Add(outputItem);
        }

        return output;
    }
}