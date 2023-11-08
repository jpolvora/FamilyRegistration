namespace FamilyRegistration.Core.Decorators;
public abstract class BaseDecorator : IScoreCalculator
{
    private readonly IScoreCalculator _wrapped;

    protected BaseDecorator(IScoreCalculator wrapped)
    {
        _wrapped = wrapped;
    }

    public virtual Task Execute(FamilyRegistrationContext context)
    {
        return _wrapped.Execute(context);
    }
}
