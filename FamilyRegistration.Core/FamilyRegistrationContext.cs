using MiddlewarePipelineLib;

namespace FamilyRegistration.Core;

public class FamilyRegistrationContext : Context
{
    public required string Key { get; set; }
    public decimal FamilyIncome { get; set; }
    public int NumOfDependents { get; set; }
    public int Score { get; private set; } = 0;
    public Dictionary<Type, int> Bag { get; } = new();

    public void IncrementScore(int valueToIncrement)
    {
        Score += valueToIncrement;
    }

}
