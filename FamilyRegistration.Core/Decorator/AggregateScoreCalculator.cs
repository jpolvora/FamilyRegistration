using FamilyRegistration.Core.Domain;

namespace FamilyRegistration.Core.Decorator;

public class AggregateScoreCalculator : AbstractScoreCalculator
{
    private readonly AbstractScoreCalculator _calculator;
    public AggregateScoreCalculator()
    {
        _calculator = new ScoreCalculator();
        _calculator = new NumOfDependentsScoreDecorator(_calculator);
        _calculator = new FamilyIncomeScoreDecorator(_calculator);

        //OR

        //_calculator = new NumOfDependentsScoreCalculator(new FamilyIncomeScoreCalculator(new InitialCalculator()));
    }


    public override Task Execute(FamilyContext context)
    {
        return _calculator.Execute(context);
    }
}
