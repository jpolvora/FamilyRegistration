namespace FamilyRegistration.Core.Composite;

public abstract class ScoreCalculatorComponent
{
    public abstract Task<int> CalculateScore(FamilyContext context);
}
