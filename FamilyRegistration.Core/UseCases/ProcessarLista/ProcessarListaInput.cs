namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public class ProcessarListaInput : List<FamilyDTO>
{
    public ProcessarListaInput(IEnumerable<FamilyDTO> items)
    {
        AddRange(items);
    }

    public ProcessarListaInput()
    {

    }

    //    public List<FamilyDTO> Data { get; } = new List<FamilyDTO>();
}
