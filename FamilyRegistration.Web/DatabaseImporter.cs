using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data;

namespace FamilyRegistration.Web;

public class DatabaseImporter
{
    private static void Main()
    {
        Execute().Wait();
        Console.WriteLine("end");
    }

    private static async Task Execute()
    {
        const string path = @"C:\Users\Jone\source\repos\FamilyRegistration";
        var fileName = Path.Combine(path, "SOLUTION_ITEMS", "MOCK_DATA.json");
        Console.WriteLine(fileName);
        IDataSource datasource = new JsonDataSource(fileName);
        IEnumerable<ProcessDataInputItem> data = await datasource.GetData(0, 1000);

        const string connectionString = "Host=localhost;Username=postgres;Password=pgsql;Database=FamilyRegistration;";
        IDataTarget target = new DBDataSource(connectionString);
        await target.BulkWrite(data.Select(s => new Pessoa()
        {
            Id = Guid.Parse(s.Key),
            Income = s.FamilyIncome,
            Dependents = s.NumOfDependents
        }).ToArray());
    }
}