using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Core.Domain;
using FamilyRegistration.Patterns.Observer;

namespace FamilyRegistration.Core.Observer.Observers;

public class FamilyIncomeObserver : GenericObserverOf<FamilyContext>
{
    public override Task HandleNotification(FamilyContext context)
    {
        var valueToIncrement = SharedCalcs.CalculateScoreByFamilyIncome(context.FamilyIncome);

        context.IncrementScore(valueToIncrement);

        return Task.CompletedTask;
    }
}

