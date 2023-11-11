using FamilyRegistration.Core.Calculators;

namespace FamilyRegistration.Core.Composite;

public class CompositeFamilyIncomeScoreCalculator : ScoreCalculatorComponent
{
    public override Task<int> CalculateScore(FamilyContext context)
    {
        var score = SharedCalcs.CalculateScoreByFamilyIncome(context.FamilyIncome);

        return Task.FromResult(score);
    }
}


