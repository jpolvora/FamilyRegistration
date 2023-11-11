using Bogus;
using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Data;

public class SampleDataGenerator : IDataSource
{
    public SampleDataGenerator() { }

    public async Task<IEnumerable<ProcessDataInputItem>> GetData(int page, int pageSize)
    {
        var result = await Generate(pageSize);
        return result;
    }

    private static Task<List<ProcessDataInputItem>> Generate(int count = 100)
    {
        var result = new List<ProcessDataInputItem>();

        var family = new Faker<ProcessDataInputItem>()
            .RuleFor(f => f.Key, f => Guid.NewGuid().ToString())
            .RuleFor(f => f.FamilyIncome, f => f.Random.Number(0, 2000))
            .RuleFor(f => f.NumOfDependents, f => f.Random.Number(1, 6))
            .Generate(count);

        result.AddRange(family);

        return Task.FromResult(result);
    }


}
