using FamilyRegistration.Core.Composite;
using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Strategy;

public class ProcessDataWithComposite : IProcessDataStrategy
{
    public async Task<ProcessDataOutput> Execute(ProcessDataInput input)
    {
        var output = new ProcessDataOutput();

        foreach (var inputItem in input)
        {
            var context = inputItem.AdaptToFamilyRegistrationContext();

            var scoreCalculators = new ScoreCalculatorGroup();

            scoreCalculators.Add(new CompositeNumOfDependentsScoreCalculator());
            scoreCalculators.Add(new CompositeFamilyIncomeScoreCalculator());

            var score = await scoreCalculators.CalculateScore(context);

            context.IncrementScore(score);

            var outputItem = context.AdaptToOutputItem();
            output.Add(outputItem);
        }





        return output;
    }

}
