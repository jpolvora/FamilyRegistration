using Bogus;
using FamilyRegistration.Core.UseCases;

namespace FamilyRegistration.Web.Routes;

public class SampleData
{
    public static List<Model> Generate(int count = 100)
    {
        var result = new List<Model>();

        var family = new Faker<Model>()
            .RuleFor(f => f.Key, f => Guid.NewGuid().ToString())
            .RuleFor(f => f.FamilyIncome, f => f.Random.Number(1, 1600))
            .RuleFor(f => f.NumOfDependents, f => f.Random.Number(1, 5))
            .Generate(count);

        result.AddRange(family);


        return result;
    }
}
