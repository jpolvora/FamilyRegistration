using FamilyRegistration.Core.Calculators;

namespace FamilyRegistration.Core.Decorator;

public class NumOfDependentsScoreDecorator : ScoreDecorator
{
    public NumOfDependentsScoreDecorator(AbstractScoreCalculator wrapped) : base(wrapped) { }

    public override Task Execute(FamilyRegistrationContext context)
    {
        var valueToIncrement = ScoreCalculators.CalculateScoreByNumOfDependents(context.NumOfDependents);

        context.IncrementScore(valueToIncrement);

        return base.Execute(context);
    }
}
