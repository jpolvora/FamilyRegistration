namespace FamilyRegistration.Core.UseCases.ProcessData;

public class ProcessDataInput : List<ProcessDataInputItem>
{
    public ProcessDataInput()
    {
    }

    public ProcessDataInput(IEnumerable<ProcessDataInputItem> items)
    {
        AddRange(items);
    }
}


