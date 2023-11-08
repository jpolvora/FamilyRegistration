namespace FamilyRegistration.Core.Decorator.Calculators;

public class FamilyIncomeScoreCalculator : ScoreDecorator
{
    public FamilyIncomeScoreCalculator(ScoreCalculator wrapped) : base(wrapped)
    {
    }

    public override Task Execute(FamilyRegistrationContext context)
    {
        var valueToIncrement = context.FamilyIncome switch
        {
            <= 900 => 5,
            > 900 and <= 1500 => 3,
            _ => 0
        };

        context.IncrementScore(valueToIncrement);

        return base.Execute(context);
    }
}