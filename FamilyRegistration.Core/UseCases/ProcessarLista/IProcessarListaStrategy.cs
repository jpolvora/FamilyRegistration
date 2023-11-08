namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public interface IProcessarListaStrategy
{
    Task<Output> Execute(Input input);
}
