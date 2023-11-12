namespace FamilyRegistration.Data;

public interface IDataTarget
{
    Task BulkWrite(Pessoa[] data);
}
