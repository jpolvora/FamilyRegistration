using FamilyRegistration.Core.Strategy;

namespace FamilyRegistration.Core.UseCases.ProcessData;

public class ProcessDataUseCase : IProcessDataUseCase
{
    private readonly IProcessDataStrategy _strategy;

    public ProcessDataUseCase(IProcessDataStrategy strategy)
    {
        _strategy = strategy;
    }

    public Task<Output> Execute(Input input) => _strategy.Execute(input);

}