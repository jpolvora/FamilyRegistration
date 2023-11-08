namespace FamilyRegistration.Core.Decorator.Calculators;

public class DummyScoreCalculator : ScoreCalculator
{
    public override Task Execute(FamilyRegistrationContext context)
    {
        return Task.CompletedTask;
    }
}
