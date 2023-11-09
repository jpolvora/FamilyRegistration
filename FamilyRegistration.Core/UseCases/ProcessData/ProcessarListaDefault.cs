namespace FamilyRegistration.Core.UseCases.ProcessData;

public class ProcessarListaDefault : IProcessDataUseCase
{
    private readonly IProcessDataStrategy _strategy;

    public ProcessarListaDefault(IProcessDataStrategy strategy)
    {
        _strategy = strategy;
    }

    public Task<Output> Execute(Input input) => _strategy.Execute(input);

}