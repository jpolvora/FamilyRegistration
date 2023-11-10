using FamilyRegistration.Core.Calculators;

namespace FamilyRegistration.Core.Decorator;

public class FamilyIncomeScoreDecorator : ScoreDecorator
{

    public FamilyIncomeScoreDecorator(AbstractScoreCalculator wrapped) : base(wrapped)
    {
    }

    public override Task Execute(FamilyRegistrationContext context)
    {
        var valueToIncrement = ScoreCalculators.CalculateScoreByFamilyIncome(context.FamilyIncome);

        context.IncrementScore(valueToIncrement);

        return base.Execute(context);
    }
}