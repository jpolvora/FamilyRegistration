using FamilyRegistration.Core.Composite;
using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Strategy;

public class ProcessDataWithComposite : IProcessDataStrategy
{
    public async Task<ProcessDataOutput> Execute(ProcessDataInput input)
    {
        var output = new ProcessDataOutput();

        var scoreCalculators = new CompositeScoreCalculator();
        scoreCalculators.Add(new CompositeNumOfDependentsScoreCalculator());
        scoreCalculators.Add(new CompositeFamilyIncomeScoreCalculator());

        foreach (var inputItem in input)
        {
            var context = inputItem.AdaptToFamilyRegistrationContext();
            await scoreCalculators.Execute(context);

            var outputItem = context.AdaptToOutputItem();
            output.Add(outputItem);
        }

        return output;
    }

}