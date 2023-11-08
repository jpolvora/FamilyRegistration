using FamilyRegistration.Core.UseCases.ProcessarLista;

namespace FamilyRegistration.Core.Datasources;
public interface IDataSource
{
    IEnumerable<FamilyDTO> GetData();
}
