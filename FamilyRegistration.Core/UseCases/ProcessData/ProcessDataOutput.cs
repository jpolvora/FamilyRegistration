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
}
