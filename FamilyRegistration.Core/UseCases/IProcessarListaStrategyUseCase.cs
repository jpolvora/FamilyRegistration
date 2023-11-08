namespace FamilyRegistration.Core.UseCases;

public interface IProcessarListaStrategyUseCase : IProcessarListaUseCase
{
    void SetStrategy(IProcessarListaUseCase useCase);
}
