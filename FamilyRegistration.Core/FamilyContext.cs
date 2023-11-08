using FamilyRegistration.Core.Pipeline;

namespace FamilyRegistration.Core;

public class FamilyContext : MiddlewareContext
{
    public int FamilyIncome { get; set; }
    public int NumOfDependents { get; set; }
    public int Score { get; private set; } = 0;

    public void IncrementScore(int valueToIncrement)
    {
        Score += valueToIncrement;
    }

}
