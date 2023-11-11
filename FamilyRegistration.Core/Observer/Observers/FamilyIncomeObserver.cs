using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Core.Observer.Observers;

public class FamilyIncomeObserver : GenericObserver<FamilyContext>
{
    public override Task Update(FamilyContext context)
    {
        var valueToIncrement = SharedCalcs.CalculateScoreByFamilyIncome(context.FamilyIncome);

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

