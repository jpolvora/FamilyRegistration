using FamilyRegistration.Core.Domain;

namespace FamilyRegistration.Core.Decorator;
public abstract class ScoreDecorator : AbstractScoreCalculator
{
    private readonly AbstractScoreCalculator _wrapped;

    public ScoreDecorator(AbstractScoreCalculator wrapped)
    {
        _wrapped = wrapped;
    }

    public override Task Execute(FamilyContext context)
    {
        return _wrapped.Execute(context);
    }
}
