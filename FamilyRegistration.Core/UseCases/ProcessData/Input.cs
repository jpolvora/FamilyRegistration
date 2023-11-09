namespace FamilyRegistration.Core.UseCases.ProcessData;

public class Input : List<InputItem>
{
    public Input(IEnumerable<InputItem> items)
    {
        AddRange(items);
    }
}
