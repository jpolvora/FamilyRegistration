using System.Data;
using Dapper;
using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.UseCases.ProcessData;
using Npgsql;

namespace FamilyRegistration.Data;

public class DBDataSource : IDataSource, IDataTarget
{
    private readonly string _connectionString;

    public DBDataSource(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection GetOpenConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }

    public async Task<IEnumerable<ProcessDataInputItem>> GetData(int page, int pageSize)
    {
        using var connection = GetOpenConnection();

        IEnumerable<ProcessDataInputItem> query = await connection
            .QueryAsync<ProcessDataInputItem>("select 'abc' as [Value] union all select @txt", new { txt = "def" });


        //var arr = query.ToArray();
        var output = new ProcessDataInput(query);

        return output;
    }

    public async Task BulkWrite(Pessoa[] data)
    {
        using var connection = GetOpenConnection();
        await connection.ExecuteAsync("insert into \"public\".\"Pessoas\" (id, income, dependents) " +
            "values(:Id, :Income, :Dependents) ", data);
    }
}
