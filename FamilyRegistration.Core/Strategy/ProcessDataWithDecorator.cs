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

        var contexts = input.Select(AdapterExtensions.Adapt);
        foreach (var ctx in contexts)
        {
            await _scoreCalculator.Execute(ctx);
            output.Add(ctx.Adapt());
        }

        return output;
    }
}