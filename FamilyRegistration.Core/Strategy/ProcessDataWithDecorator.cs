using FamilyRegistration.Core.Decorator;
using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Core.Strategy;

public class ProcessDataWithDecorator : IProcessDataStrategy
{
    private readonly AbstractScoreCalculator _scoreCalculator;

    public ProcessDataWithDecorator(AbstractScoreCalculator scoreCalculator)
    {
        _scoreCalculator = scoreCalculator;
    }

    public async Task<Output> Execute(Input input)
    {
        var output = new Output();

        foreach (var inputItem in input)
        {
            var context = inputItem.AdaptToFamilyRegistrationContext();
            await _scoreCalculator.Execute(context);

            var outputItem = context.AdaptToOutputItem();
            output.Add(outputItem);
        }

        return output;
    }
}