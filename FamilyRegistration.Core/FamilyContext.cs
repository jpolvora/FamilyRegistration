using MiddlewarePipelineLib;

namespace FamilyRegistration.Core;

public class FamilyContext : Context
{
    public required string Key { get; set; }
    public int FamilyIncome { get; set; }
    public int NumOfDependents { get; set; }
    public int Score { get; private set; } = 0;

    public void IncrementScore(int valueToIncrement)
    {
        Score += valueToIncrement;
    }

}
