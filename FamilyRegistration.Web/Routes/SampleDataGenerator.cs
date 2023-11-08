using Bogus;
using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.UseCases.ProcessarLista;

namespace FamilyRegistration.Web.Routes;

public class SampleDataGenerator : IDataSource
{
    public int Count { get; }

    public SampleDataGenerator(int count)
    {
        Count = count;
    }

    public IEnumerable<FamilyDTO> GetData()
    {
        return Generate(Count);
    }

    private static List<FamilyDTO> Generate(int count = 100)
    {
        var result = new List<FamilyDTO>();

        var family = new Faker<FamilyDTO>()
            .RuleFor(f => f.Key, f => Guid.NewGuid().ToString())
            .RuleFor(f => f.FamilyIncome, f => f.Random.Number(1, 1600))
            .RuleFor(f => f.NumOfDependents, f => f.Random.Number(1, 5))
            .Generate(count);

        result.AddRange(family);


        return result;
    }


}
