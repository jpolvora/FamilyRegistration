namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public class Input : List<InputItem>
{
    public Input(IEnumerable<InputItem> items)
    {
        AddRange(items);
    }
}
