using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Strategy;

public class ProcessDataWithNoDesignPattern : IProcessDataStrategy
{
    public Task<Output> Execute(Input input)
    {
        var output = new Output();

        var contexts = input.Select(AdapterExtensions.Adapt);
        foreach (var ctx in contexts)
        {
            int score = 0;
            score += ScoreCalculators.CalculateScoreByFamilyIncome(ctx.FamilyIncome);
            score += ScoreCalculators.CalculateScoreByNumOfDependents(ctx.NumOfDependents);
            ctx.IncrementScore(score);
            output.Add(ctx.Adapt());
        }

        return Task.FromResult(output);
    }
}
