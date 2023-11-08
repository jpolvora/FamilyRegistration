namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public class ProcessarListaDefault : IProcessarListaUseCase
{

    public async Task<Output> Execute(Input input)
    {
        var output = new Output();
        return await Task.FromResult(output);
    }
}