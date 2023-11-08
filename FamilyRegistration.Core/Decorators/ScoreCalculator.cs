namespace FamilyRegistration.Core.Decorators;

public sealed class ScoreCalculator : IScoreCalculator
{
    public Task Execute(FamilyRegistrationContext context)
    {
        return Task.CompletedTask;
    }
}
