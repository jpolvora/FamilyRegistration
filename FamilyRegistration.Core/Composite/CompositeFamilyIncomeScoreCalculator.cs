using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Core.Domain;
using FamilyRegistration.Patterns.Composite;

namespace FamilyRegistration.Core.Composite;

public class CompositeFamilyIncomeScoreCalculator : AbstractComponent<FamilyContext>
{
    public override Task Execute(FamilyContext context)
    {
        var score = SharedCalcs.CalculateScoreByFamilyIncome(context.FamilyIncome);
        context.IncrementScore(score);

        return Task.CompletedTask;
    }
}


