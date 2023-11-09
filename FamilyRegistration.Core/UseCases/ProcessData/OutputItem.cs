namespace FamilyRegistration.Core.UseCases.ProcessData;

public class OutputItem
{
    public required string Key { get; set; }
    public int FamilyIncome { get; set; }
    public int NumOfDependents { get; set; }

    public required int Score { get; set; }

}
