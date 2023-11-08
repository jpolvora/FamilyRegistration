namespace FamilyRegistration.Core.Decorator;
public abstract class ScoreCalculator
{
    public abstract Task Execute(FamilyRegistrationContext context);
}
