using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Strategy;

public class ProcessDataWithTransactionScript : IProcessDataStrategy
{
    public Task<Output> Execute(Input input)
    {
        var output = new Output();

        foreach (var inputItem in input)
        {
            var context = inputItem.AdaptToFamilyRegistrationContext();

            int score = 0;
            score += ScoreCalculators.CalculateScoreByFamilyIncome(context.FamilyIncome);
            score += ScoreCalculators.CalculateScoreByNumOfDependents(context.NumOfDependents);
            context.IncrementScore(score);

            var outputItem = context.AdaptToOutputItem();
            output.Add(outputItem);
        }

        return Task.FromResult(output);
    }
}
