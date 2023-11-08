namespace FamilyRegistration.Core.Decorators;
public interface IScoreCalculator
{
    Task Execute(FamilyRegistrationContext context);
}
