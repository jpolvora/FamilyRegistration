namespace FamilyRegistration.Core.UseCases.ProcessData;

public class ProcessDataOutput : List<ProcessDataOutputItem>
{
    public ProcessDataOutput(IEnumerable<ProcessDataOutputItem> items)
    {
        AddRange(items);
    }

    public ProcessDataOutput()
    {

    }

    public override string ToString()
    {
        string combinedString = string.Join(",", this);
        return combinedString;
    }
}
