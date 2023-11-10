using FamilyRegistration.Core.Calculators;

namespace FamilyRegistration.Core.Observer.Observers;

public class NumOfDependentsObserver : AbstractObserver
{
    public NumOfDependentsObserver(ISubject<FamilyRegistrationContext> subject) : base(subject)
    {
    }

    public NumOfDependentsObserver() : base()
    {

    }

    public override async Task Update(FamilyRegistrationContext? context)
    {
        if (context == null) return;

        var valueToIncrement = SharedCalcs.CalculateScoreByNumOfDependents(context.NumOfDependents);

        context.IncrementScore(valueToIncrement);

        await Task.CompletedTask;
    }
}

