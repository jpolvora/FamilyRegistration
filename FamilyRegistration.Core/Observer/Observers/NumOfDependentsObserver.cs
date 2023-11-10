using FamilyRegistration.Core.Calculators;

namespace FamilyRegistration.Core.Observer.Observers;

public class NumOfDependentsObserver : AbstractObserver
{
    public override void Update(FamilyRegistrationContext? context)
    {
        if (context == null) return;

        var valueToIncrement = ScoreCalculators.CalculateScoreByNumOfDependents(context.NumOfDependents);

        context.IncrementScore(valueToIncrement);
    }
}

