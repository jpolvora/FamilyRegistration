using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Core.Observer.Observers;

public class NumOfDependentsObserver : GenericObserver<FamilyContext>
{
    public override Task Update(FamilyContext context)
    {
        var valueToIncrement = SharedCalcs.CalculateScoreByNumOfDependents(context.NumOfDependents);

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

