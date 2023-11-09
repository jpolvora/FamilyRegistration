using Bogus;
using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Data;

public class SampleDataGenerator : IDataSource
{
    public int Count { get; }

    public SampleDataGenerator(int count)
    {
        Count = count;
    }

    public IEnumerable<InputItem> GetData()
    {
        return Generate(Count);
    }

    private static List<InputItem> Generate(int count = 100)
    {
        var result = new List<InputItem>();

        var family = new Faker<InputItem>()
            .RuleFor(f => f.Key, f => Guid.NewGuid().ToString())
            .RuleFor(f => f.FamilyIncome, f => f.Random.Number(0, 2000))
            .RuleFor(f => f.NumOfDependents, f => f.Random.Number(1, 6))
            .Generate(count);

        result.AddRange(family);


        return result;
    }


}
