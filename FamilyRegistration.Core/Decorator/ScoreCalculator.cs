namespace FamilyRegistration.Core.Decorator;

public class ScoreCalculator : AbstractScoreCalculator
{
    public override Task Execute(FamilyRegistrationContext context)
    {
        return Task.CompletedTask;
    }
}
