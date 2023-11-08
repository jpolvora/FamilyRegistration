using Bogus;
using FamilyRegistration.Core.UseCases.ProcessarLista;

namespace FamilyRegistration.Web.Routes;

public class SampleDataGenerator
{
    public static List<FamilyDTO> Generate(int count = 100)
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
