using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Strategy;

public interface IProcessDataStrategy
{
    Task<ProcessDataOutput> Execute(ProcessDataInput input);
}
