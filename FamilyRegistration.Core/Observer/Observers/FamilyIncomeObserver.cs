using FamilyRegistration.Core.Calculators;

namespace FamilyRegistration.Core.Observer.Observers;

public class FamilyIncomeObserver : AbstractObserver
{
    public FamilyIncomeObserver()
    {
    }

    public FamilyIncomeObserver(ISubject<FamilyRegistrationContext> subject) : base(subject)
    {
    }

    public override async Task Update(FamilyRegistrationContext? context)
    {
        if (context == null) return;

        var valueToIncrement = SharedCalcs.CalculateScoreByFamilyIncome(context.FamilyIncome);

        context.IncrementScore(valueToIncrement);

        await Task.CompletedTask;
    }
}

