using FamilyRegistration.Core.Calculators;

namespace FamilyRegistration.Core.Observer.Observers;

public class NumOfDependentsObserver : FamilyRegistrationContextObserver
{
    public override Task Update(FamilyContext context)
    {
        var valueToIncrement = SharedCalcs.CalculateScoreByNumOfDependents(context.NumOfDependents);

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

