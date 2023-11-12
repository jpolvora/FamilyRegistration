using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.UseCases.ProcessData;
using Newtonsoft.Json;

namespace FamilyRegistration.Data;

public class JsonDataSource : IDataSource
{
    private readonly string _filePath;

    public JsonDataSource(string filePath)
    {
        _filePath = filePath;
    }
    public async Task<IEnumerable<ProcessDataInputItem>> GetData(int page, int pageSize)
    {
        var file = await File.ReadAllTextAsync(_filePath);
        var jsonData = JsonConvert.DeserializeObject<JsonFormatOne[]>(file);
        if (jsonData is not null)
        {
            var input = jsonData.Select(s => s.Adapt()).AsInput();

            return input;
        }

        return new ProcessDataInput();
    }
}
