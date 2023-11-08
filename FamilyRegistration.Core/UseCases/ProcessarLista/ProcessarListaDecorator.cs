using FamilyRegistration.Core.Decorator;

namespace FamilyRegistration.Core.UseCases.ProcessarLista;

public class ProcessarListaDecorator : IProcessarListaUseCase
{
    private readonly ScoreCalculator _calculator;

    public ProcessarListaDecorator(ScoreCalculator calculator)
    {
        _calculator = calculator;
    }

    public async Task<Output> Execute(Input input)
    {
        var contexts = input.Select(AdapterExtensions.Adapt);
        var output = new Output();

        foreach (var ctx in contexts)
        {
            await _calculator.Execute(ctx);
            output.Add(ctx.Adapt());
        }

        return output;
    }
}
