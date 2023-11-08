using FamilyRegistration.Core.Decorator;

namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public class ProcessaListaWithDecoratorStrategy : IProcessarListaStrategy
{
    private readonly ScoreCalculator _scoreCalculator;

    public ProcessaListaWithDecoratorStrategy(ScoreCalculator scoreCalculator)
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