using FamilyRegistration.Core.Calculators;
using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Strategy;

public class ProcessDataWithTransactionScript : IProcessDataStrategy
{
    public Task<ProcessDataOutput> Execute(ProcessDataInput input)
    {
        var output = new ProcessDataOutput();

        foreach (var inputItem in input)
        {
            var context = inputItem.AdaptToFamilyRegistrationContext();

            int score = 0;
            score += SharedCalcs.CalculateScoreByFamilyIncome(context.FamilyIncome);
            score += SharedCalcs.CalculateScoreByNumOfDependents(context.NumOfDependents);
            context.IncrementScore(score);

            var outputItem = context.AdaptToOutputItem();
            output.Add(outputItem);
        }

        return Task.FromResult(output);
    }
}
