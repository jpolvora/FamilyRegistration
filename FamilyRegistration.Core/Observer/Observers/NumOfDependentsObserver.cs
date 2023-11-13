using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Core.Domain;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Core.Observer.Observers;

public class NumOfDependentsObserver : GenericObserverOf<FamilyContext>
{
    public override Task HandleNotification(FamilyContext context)
    {
        var valueToIncrement = SharedCalcs.CalculateScoreByNumOfDependents(context.NumOfDependents);

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

