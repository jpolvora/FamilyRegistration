namespace FamilyRegistration.Core.Decorator.Calculators;
public abstract class ScoreCalculator
{
    public abstract Task Execute(FamilyRegistrationContext context);
}
