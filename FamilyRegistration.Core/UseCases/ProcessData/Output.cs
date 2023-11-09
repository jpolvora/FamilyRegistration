namespace FamilyRegistration.Core.UseCases.ProcessData;

public class Output : List<OutputItem>
{
    public Output(IEnumerable<OutputItem> items)
    {
        AddRange(items);
    }

    public Output()
    {

    }
}
