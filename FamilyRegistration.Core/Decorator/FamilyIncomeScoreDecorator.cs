using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Core.Domain;

namespace FamilyRegistration.Core.Decorator;

public class FamilyIncomeScoreDecorator : ScoreDecorator
{

    public FamilyIncomeScoreDecorator(AbstractScoreCalculator wrapped) : base(wrapped)
    {
    }

    public override Task Execute(FamilyContext context)
    {
        var valueToIncrement = SharedCalcs.CalculateScoreByFamilyIncome(context.FamilyIncome);

        context.IncrementScore(valueToIncrement);

        return base.Execute(context);
    }
}