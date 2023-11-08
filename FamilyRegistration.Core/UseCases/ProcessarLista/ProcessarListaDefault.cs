namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public class ProcessarListaDefault : IProcessarListaUseCase
{
    private readonly IProcessarListaStrategy _strategy;

    public ProcessarListaDefault(IProcessarListaStrategy strategy)
    {
        _strategy = strategy;
    }

    public Task<Output> Execute(Input input) => _strategy.Execute(input);

}