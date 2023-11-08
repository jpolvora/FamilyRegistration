using FamilyRegistration.Core.Decorator.Calculators;

namespace FamilyRegistration.Core.Decorator;

public class AggregateScoreCalculator : ScoreCalculator
{
    private readonly ScoreCalculator _calculator;
    public AggregateScoreCalculator()
    {
        ScoreCalculator scoreCalculator = new DummyScoreCalculator();
        scoreCalculator = new NumOfDependentsScoreCalculator(scoreCalculator);
        scoreCalculator = new FamilyIncomeScoreCalculator(scoreCalculator);

        _calculator = scoreCalculator;
    }


    public override Task Execute(FamilyRegistrationContext context)
    {
        return _calculator.Execute(context);
    }
}
