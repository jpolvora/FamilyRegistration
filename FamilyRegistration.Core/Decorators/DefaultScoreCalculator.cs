namespace FamilyRegistration.Core.Decorators;

public class DefaultScoreCalculator : IScoreCalculator
{
    private readonly IScoreCalculator _calculator;
    public DefaultScoreCalculator()
    {
        IScoreCalculator scoreCalculator = new ScoreCalculator();
        scoreCalculator = new NumOfDependentsDecorator(scoreCalculator);
        scoreCalculator = new FamilyIncomeDecorator(scoreCalculator);

        this._calculator = scoreCalculator;
    }

    public Task Execute(FamilyRegistrationContext context)
    {
        return this._calculator.Execute(context);
    }
}
