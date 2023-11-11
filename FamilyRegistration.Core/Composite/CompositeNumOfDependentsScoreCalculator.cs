using FamilyRegistration.Core.Calculators;

namespace FamilyRegistration.Core.Composite;

public class CompositeNumOfDependentsScoreCalculator : ScoreCalculatorComponent
{
    public override Task<int> CalculateScore(FamilyContext context)
    {
        var score = SharedCalcs.CalculateScoreByNumOfDependents(context.NumOfDependents);

        return Task.FromResult(score);
    }
}


