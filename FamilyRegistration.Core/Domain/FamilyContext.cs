namespace FamilyRegistration.Core.Domain;

public class FamilyContext
{

    public required string Key { get; set; }
    public decimal FamilyIncome { get; set; }
    public int NumOfDependents { get; set; }
    public int Score { get; private set; } = 0;

    public void IncrementScore(int valueToIncrement)
    {
        Score += valueToIncrement;
    }

}
