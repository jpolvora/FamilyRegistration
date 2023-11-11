namespace FamilyRegistration.Core.Decorator;

public class ScoreCalculator : AbstractScoreCalculator
{
    public override Task Execute(FamilyContext context)
    {
        return Task.CompletedTask;
    }
}
