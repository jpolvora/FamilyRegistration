namespace FamilyRegistration.Core.UseCases.ProcessData;

public class InputItem
{
    public required string Key { get; set; }
    public int FamilyIncome { get; set; }
    public int NumOfDependents { get; set; }

}
