using FamilyRegistration.Core.Observer;
using FamilyRegistration.Core.Observer.Observers;
using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Strategy;

public class ProcessDataWithObservers : IProcessDataStrategy
{
    public Task<Output> Execute(Input input)
    {
        var output = new Output();

        using var subject = new ConcreteSubject();

        subject.Attach(new NumOfDependentsObserver());
        subject.Attach(new FamilyIncomeObserver());

        foreach (var inputItem in input)
        {
            var context = inputItem.AdaptToFamilyRegistrationContext();

            //will trigger update
            subject.SetState(context);

            var outputItem = context.AdaptToOutputItem();
            output.Add(outputItem);
        }

        return Task.FromResult(output);
    }
}