using FamilyRegistration.Core.Calculators;

namespace FamilyRegistration.Core.Observer.Observers;

public class FamilyIncomeObserver : AbstractObserver
{
    public override void Update(FamilyRegistrationContext? context)
    {
        if (context == null) return;

        var valueToIncrement = ScoreCalculators.CalculateScoreByFamilyIncome(context.FamilyIncome);

        context.IncrementScore(valueToIncrement);
    }
}

