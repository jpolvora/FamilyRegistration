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

    public override void Update(FamilyRegistrationContext? context)
    {
        if (context == null) return;

        var valueToIncrement = ScoreCalculators.CalculateScoreByNumOfDependents(context.NumOfDependents);

        context.IncrementScore(valueToIncrement);
    }
}

