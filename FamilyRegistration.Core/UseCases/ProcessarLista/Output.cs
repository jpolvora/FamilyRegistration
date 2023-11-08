namespace FamilyRegistration.Core.UseCases.ProcessarLista;

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
