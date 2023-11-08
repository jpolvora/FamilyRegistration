namespace FamilyRegistration.Core.UseCases;

public class ProcessarListaInput
{
    public ProcessarListaInput(IEnumerable<Model> items)
    {
        Data.AddRange(items);
    }

    public ProcessarListaInput()
    {

    }

    public List<Model> Data { get; } = new List<Model>();
}
