using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Patterns.Composite;

namespace FamilyRegistration.Core.Composite;

public class CompositeNumOfDependentsScoreCalculator : AbstractComponent<FamilyContext>
{
    public override Task Execute(FamilyContext context)
    {
        var score = SharedCalcs.CalculateScoreByNumOfDependents(context.NumOfDependents);
        context.IncrementScore(score);

        return Task.CompletedTask;
    }
}


