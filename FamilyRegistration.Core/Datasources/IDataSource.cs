using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Datasources;
public interface IDataSource
{
    Task<IEnumerable<InputItem>> GetData(int page, int pageSize);
}
