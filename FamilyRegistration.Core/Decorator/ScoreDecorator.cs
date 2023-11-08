namespace FamilyRegistration.Core.Decorator;
public abstract class ScoreDecorator : ScoreCalculator
{
    private readonly ScoreCalculator _wrapped;

    public ScoreDecorator(ScoreCalculator wrapped)
    {
        _wrapped = wrapped;
    }

    public override Task Execute(FamilyRegistrationContext context)
    {
        return _wrapped.Execute(context);
    }
}
