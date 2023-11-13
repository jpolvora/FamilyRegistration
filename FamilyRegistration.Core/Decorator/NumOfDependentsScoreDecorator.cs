using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Core.Domain;

namespace FamilyRegistration.Core.Decorator;

public class NumOfDependentsScoreDecorator : ScoreDecorator
{
    public NumOfDependentsScoreDecorator(AbstractScoreCalculator wrapped) : base(wrapped) { }

    public override Task Execute(FamilyContext context)
    {
        var valueToIncrement = SharedCalcs.CalculateScoreByNumOfDependents(context.NumOfDependents);

        context.IncrementScore(valueToIncrement);

        return base.Execute(context);
    }
}
