using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Datasources;
public interface IDataSource
{
    Task<IEnumerable<ProcessDataInputItem>> GetData(int page, int pageSize);
}
