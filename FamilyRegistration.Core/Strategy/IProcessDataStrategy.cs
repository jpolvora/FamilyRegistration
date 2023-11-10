using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Strategy;

public interface IProcessDataStrategy
{
    Task<Output> Execute(Input input);
}
