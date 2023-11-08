using FamilyRegistration.Core.UseCases.ProcessarLista;

namespace FamilyRegistration.Core.UseCases;

public class ProcessarListaStrategy : IProcessarListaStrategyUseCase
{
    private IProcessarListaUseCase? _useCase;

    public void SetStrategy(IProcessarListaUseCase useCase)
    {
        this._useCase = useCase;
    }

    public Task<ProcessarListaOutput> Execute(ProcessarListaInput input)
    {
        return this._useCase == null ? throw new Exception("useCase has not been set") : _useCase.Execute(input);
    }
}