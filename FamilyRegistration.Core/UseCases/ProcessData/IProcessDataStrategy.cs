namespace FamilyRegistration.Core.UseCases.ProcessData;

public interface IProcessDataStrategy
{
    Task<Output> Execute(Input input);
}
