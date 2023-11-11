using FamilyRegistration.Core.Calculators;

namespace FamilyRegistration.Core.Observer.Observers;

public class FamilyIncomeObserver : FamilyRegistrationContextObserver
{

    public override Task Update(FamilyContext context)
    {
        var valueToIncrement = SharedCalcs.CalculateScoreByFamilyIncome(context.FamilyIncome);

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

