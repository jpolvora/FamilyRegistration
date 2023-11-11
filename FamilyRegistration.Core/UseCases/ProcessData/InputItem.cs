namespace FamilyRegistration.Core.UseCases.ProcessData;

public class ProcessDataInputItem
{
    public required string Key { get; set; }
    public decimal FamilyIncome { get; set; }
    public int NumOfDependents { get; set; }

}
